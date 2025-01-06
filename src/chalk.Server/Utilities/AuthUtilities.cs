using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using chalk.Server.Configurations;
using chalk.Server.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace chalk.Server.Utilities;

public static class AuthUtilities
{
    private const string Alg = SecurityAlgorithms.HmacSha256;

    public enum TokenType
    {
        AccessToken,
        RefreshToken
    }

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

    public static string CreateAccessToken(TokenDTO tokenData, ClaimDTO claimData)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = CreateClaims(claimData),
            Issuer = tokenData.Issuer,
            Audience = tokenData.Audience,
            Expires = DateTime.UtcNow.AddMinutes(15),
            NotBefore = DateTime.UtcNow,
            SigningCredentials = new SigningCredentials(CreateKey(tokenData.Key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static string CreateRefreshToken()
    {
        var bytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }

    public static string? GetUserId(this string accessToken, TokenDTO tokenData)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false, // Accept expired tokens
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenData.Issuer,
            ValidAudience = tokenData.Audience,
            IssuerSigningKey = CreateKey(tokenData.Key),
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(Alg, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new ServiceException("Access token is invalid.", StatusCodes.Status401Unauthorized);
        }

        return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public static string GetUserId(this ClaimsPrincipal principal)
    {
        var id = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (id is null)
        {
            throw new ServiceException("Not logged in.", StatusCodes.Status401Unauthorized);
        }

        return id;
    }

    private static SymmetricSecurityKey CreateKey(string securityKey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
    }

    private static ClaimsIdentity CreateClaims(ClaimDTO claimData)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, claimData.UserId.ToString()));
        claims.AddClaim(new Claim(ClaimTypes.Name, claimData.DisplayName));
        claims.AddClaims(claimData.Roles.Select(role => new Claim(ClaimTypes.Role, role)));
        return claims;
    }
}