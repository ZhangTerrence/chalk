using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using chalk.Server.Common;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace chalk.Server.Services;

public class AuthenticationService : IAuthenticationService
{
    private static readonly DateTimeOffset AccessTokenExpiryDate = DateTimeOffset.UtcNow.AddHours(1);
    private static readonly DateTimeOffset RefreshTokenExpiryDate = DateTimeOffset.UtcNow.AddDays(1);

    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly SymmetricSecurityKey _securityKey;

    public AuthenticationService(
        IConfiguration configuration,
        UserManager<User> userManager,
        RoleManager<IdentityRole<long>> roleManager,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _configuration = configuration;
        _userManager = userManager;
        _roleManager = roleManager;
        _httpContextAccessor = httpContextAccessor;
        _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
    }

    public async Task<AuthResponse> RegisterUserAsync(RegisterRequest registerRequest)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerRequest.Email!);
        if (existingUser is not null)
        {
            throw new BadHttpRequestException("User already exists.", StatusCodes.Status409Conflict);
        }

        var user = registerRequest.ToEntity();

        var createdUser = await _userManager.CreateAsync(user, registerRequest.Password!);
        if (!createdUser.Succeeded)
        {
            throw new BadHttpRequestException("Unable to create user.", StatusCodes.Status500InternalServerError);
        }

        if (!await _roleManager.RoleExistsAsync("User"))
        {
            var createdRole = await _roleManager.CreateAsync(new IdentityRole<long>("User"));
            if (!createdRole.Succeeded)
            {
                throw new BadHttpRequestException("Unable to create user role.",
                    StatusCodes.Status500InternalServerError);
            }
        }

        var assignedUser = await _userManager.AddToRoleAsync(user, "User");
        if (!assignedUser.Succeeded)
        {
            throw new BadHttpRequestException("Unable to assign user to user role.",
                StatusCodes.Status500InternalServerError);
        }

        var accessToken = CreateAccessToken(user.Id, user.DisplayName, ["User"]);
        var refreshToken = CreateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryDate = RefreshTokenExpiryDate.DateTime.ToUniversalTime();
        var updatedUser = await _userManager.UpdateAsync(user);
        if (!updatedUser.Succeeded)
        {
            throw new BadHttpRequestException("Unable to set refresh token.", StatusCodes.Status500InternalServerError);
        }

        AddCookie(TokenType.AccessToken, accessToken);
        AddCookie(TokenType.RefreshToken, refreshToken);

        return new AuthResponse(user.ToResponse(), accessToken, refreshToken);
    }

    public async Task<AuthResponse> LoginUserAsync(LoginRequest loginRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);
        if (user is null)
        {
            throw new BadHttpRequestException("User not found.", StatusCodes.Status404NotFound);
        }

        var authenticated = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
        if (!authenticated)
        {
            throw new BadHttpRequestException("Invalid credentials.", StatusCodes.Status401Unauthorized);
        }

        var roles = await _userManager.GetRolesAsync(user);

        var accessToken = CreateAccessToken(user.Id, user.DisplayName, roles);
        var refreshToken = CreateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryDate = RefreshTokenExpiryDate.DateTime.ToUniversalTime();
        var updatedUser = await _userManager.UpdateAsync(user);
        if (!updatedUser.Succeeded)
        {
            throw new BadHttpRequestException("Unable to set refresh token.", StatusCodes.Status500InternalServerError);
        }

        AddCookie(TokenType.AccessToken, accessToken);
        AddCookie(TokenType.RefreshToken, refreshToken);

        return new AuthResponse(user.ToResponse(), accessToken, refreshToken);
    }

    public async Task LogoutUserAsync(ClaimsPrincipal identity)
    {
        var currentUserId = identity.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserId is null)
        {
            throw new BadHttpRequestException("Unauthorized.", StatusCodes.Status401Unauthorized);
        }

        var currentUser = await _userManager.FindByIdAsync(currentUserId);
        if (currentUser is null)
        {
            throw new BadHttpRequestException("User not found.", StatusCodes.Status404NotFound);
        }

        currentUser.RefreshToken = null;
        currentUser.RefreshTokenExpiryDate = null;
        await _userManager.UpdateAsync(currentUser);
    }

    public async Task RefreshTokensAsync(ClaimsPrincipal identity, string? refreshToken)
    {
        var currentUserId = identity.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserId is null || refreshToken is null)
        {
            throw new BadHttpRequestException("Unauthorized.", StatusCodes.Status401Unauthorized);
        }

        var currentUser = await _userManager.FindByIdAsync(currentUserId);
        if (currentUser is null)
        {
            throw new BadHttpRequestException("User not found.", StatusCodes.Status404NotFound);
        }

        if (currentUser.RefreshTokenExpiryDate < DateTime.UtcNow)
        {
            throw new BadHttpRequestException("Refresh token is expired.", StatusCodes.Status401Unauthorized);
        }

        if (currentUser.RefreshToken != refreshToken)
        {
            throw new BadHttpRequestException("Refresh token is invalid.", StatusCodes.Status401Unauthorized);
        }

        var roles = await _userManager.GetRolesAsync(currentUser);

        var newAccessToken = CreateAccessToken(currentUser.Id, currentUser.DisplayName, roles);
        var newRefreshToken = CreateRefreshToken();

        currentUser.RefreshToken = newRefreshToken;
        currentUser.RefreshTokenExpiryDate = RefreshTokenExpiryDate.DateTime.ToUniversalTime();
        var updatedUser = await _userManager.UpdateAsync(currentUser);
        if (!updatedUser.Succeeded)
        {
            throw new BadHttpRequestException("Unable to set refresh token.", StatusCodes.Status500InternalServerError);
        }

        AddCookie(TokenType.AccessToken, newAccessToken);
        AddCookie(TokenType.RefreshToken, newRefreshToken);
    }

    private string CreateAccessToken(long userId, string displayName, IEnumerable<string> roles)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = CreateClaims(userId, displayName, roles),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            Expires = AccessTokenExpiryDate.DateTime.ToUniversalTime(),
            SigningCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static ClaimsIdentity CreateClaims(long userId, string displayName, IEnumerable<string> roles)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
        claims.AddClaim(new Claim(ClaimTypes.Name, displayName));
        claims.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        return claims;
    }

    private static string CreateRefreshToken()
    {
        var bytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }

    private void AddCookie(TokenType tokenType, string token)
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(ToString(tokenType), token, new CookieOptions
        {
            Expires = tokenType == TokenType.AccessToken ? AccessTokenExpiryDate : RefreshTokenExpiryDate,
            Path = "/api",
            HttpOnly = true,
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.Strict,
        });
    }

    private static string ToString(TokenType tokenType)
    {
        switch (tokenType)
        {
            case TokenType.AccessToken:
                return "AccessToken";
            case TokenType.RefreshToken:
                return "RefreshToken";
            default:
                throw new ArgumentOutOfRangeException(nameof(tokenType), tokenType, null);
        }
    }
}