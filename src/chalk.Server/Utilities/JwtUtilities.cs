using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using chalk.Server.Configurations;
using chalk.Server.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace chalk.Server.Utilities;

public static class JwtUtilities
{
    public enum TokenType
    {
        AccessToken,
        RefreshToken
    }

    private const string Alg = SecurityAlgorithms.HmacSha256;

    public static string GetString(this TokenType tokenType)
    {
        switch (tokenType)
        {
            case TokenType.AccessToken:
                return "AccessToken";
            case TokenType.RefreshToken:
                return "RefreshToken";
            default:
                throw new ServiceException("Unrecognized token type.", StatusCodes.Status500InternalServerError);
        }
    }

    public static string CreateAccessToken(JwtTokenInfoDTO tokenInfo, JwtClaimInfoDTO claimInfo)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = CreateClaims(claimInfo),
            Issuer = tokenInfo.Issuer,
            Audience = tokenInfo.Audience,
            Expires = DateTime.UtcNow.AddMinutes(15),
            NotBefore = DateTime.UtcNow,
            SigningCredentials = new SigningCredentials(tokenInfo.SecurityKey, SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static ClaimsIdentity CreateClaims(JwtClaimInfoDTO claimInfo)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, claimInfo.UserId.ToString()));
        claims.AddClaim(new Claim(ClaimTypes.Name, claimInfo.DisplayName));
        claims.AddClaims(claimInfo.Roles.Select(role => new Claim(ClaimTypes.Role, role)));
        return claims;
    }

    public static string CreateRefreshToken()
    {
        var bytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }

    public static string? GetUserIdFromExpiredAccessToken(JwtTokenInfoDTO tokenInfo, string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenInfo.Issuer,
            ValidAudience = tokenInfo.Audience,
            IssuerSigningKey = tokenInfo.SecurityKey,
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);

        if (
            securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(Alg, StringComparison.InvariantCultureIgnoreCase)
        )
        {
            throw new ServiceException("Access token is invalid.", StatusCodes.Status401Unauthorized);
        }

        return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}