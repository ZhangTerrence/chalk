using chalk.Server.Configurations;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Requests;
using chalk.Server.Entities;
using chalk.Server.Services.Interfaces;
using chalk.Server.Utilities;
using Microsoft.AspNetCore.Identity;

namespace chalk.Server.Services;

public class AuthService : IAuthService
{
    private readonly TokenDTO _tokenData;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IConfiguration configuration, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _tokenData = new TokenDTO(configuration["Jwt:Issuer"]!, configuration["Jwt:Audience"]!, configuration["Jwt:Key"]!);
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<(long, string, string)> CreateTokensAsync(User user, IEnumerable<string> roles)
    {
        var accessToken = AuthUtilities.CreateAccessToken(_tokenData, new ClaimDTO(user.Id, user.DisplayName, roles));
        var refreshToken = AuthUtilities.CreateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryDate = DateTime.UtcNow.AddHours(2);
        if (!(await _userManager.UpdateAsync(user)).Succeeded)
        {
            throw new ServiceException("Unable to set refresh token.", StatusCodes.Status500InternalServerError);
        }

        AddCookie(AuthUtilities.TokenType.AccessToken, accessToken);
        AddCookie(AuthUtilities.TokenType.RefreshToken, refreshToken);

        return (user.Id, accessToken, refreshToken);
    }

    public async Task<(long, string, string)> AuthenticateAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email!);
        if (user is null)
        {
            throw new ServiceException("User not found.", StatusCodes.Status404NotFound);
        }

        var correct = await _userManager.CheckPasswordAsync(user, request.Password!);
        if (!correct)
        {
            throw new ServiceException("Invalid credentials.", StatusCodes.Status401Unauthorized);
        }

        var roles = await _userManager.GetRolesAsync(user);

        return await CreateTokensAsync(user, roles);
    }

    public async Task<(long, string, string)> RefreshTokenAsync(string? accessToken, string? refreshToken)
    {
        if (accessToken is null || refreshToken is null)
        {
            throw new ServiceException("Missing tokens.", StatusCodes.Status401Unauthorized);
        }

        var userId = accessToken.GetUserId(_tokenData);

        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            throw new ServiceException("User not found.", StatusCodes.Status404NotFound);
        }

        if (user.RefreshToken != refreshToken)
        {
            throw new ServiceException("Refresh token is invalid.", StatusCodes.Status401Unauthorized);
        }

        if (user.RefreshTokenExpiryDate < DateTime.UtcNow)
        {
            throw new ServiceException("Refresh token is expired.", StatusCodes.Status401Unauthorized);
        }

        var roles = await _userManager.GetRolesAsync(user);
        var newAccessToken = AuthUtilities.CreateAccessToken(_tokenData, new ClaimDTO(user.Id, user.DisplayName, roles));

        AddCookie(AuthUtilities.TokenType.AccessToken, newAccessToken);

        return (user.Id, newAccessToken, refreshToken);
    }

    public async Task DeleteTokensAsync(long userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            throw new ServiceException("User not found.", StatusCodes.Status404NotFound);
        }

        user.RefreshToken = null;
        user.RefreshTokenExpiryDate = null;
        var updatedUser = await _userManager.UpdateAsync(user);
        if (!updatedUser.Succeeded)
        {
            throw new ServiceException("Unable to delete refresh token.", StatusCodes.Status500InternalServerError);
        }

        DeleteCookie("AccessToken");
        DeleteCookie("RefreshToken");
    }

    private void AddCookie(AuthUtilities.TokenType tokenType, string token)
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(tokenType.CookieName(), token, new CookieOptions
        {
            Expires = DateTime.UtcNow.AddDays(1),
            Path = "/api",
            HttpOnly = true,
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.Strict,
        });
    }

    private void DeleteCookie(string cookie)
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete(cookie);
    }
}