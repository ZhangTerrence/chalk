using System.Security.Claims;
using System.Text;
using chalk.Server.Configurations;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
using chalk.Server.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace chalk.Server.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    private readonly JwtTokenInfoDTO _jwtTokenInfo;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationService(
        UserManager<User> userManager,
        RoleManager<IdentityRole<long>> roleManager,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;

        _jwtTokenInfo = new JwtTokenInfoDTO(
            configuration["Jwt:Issuer"]!,
            configuration["Jwt:Audience"]!,
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
        );

        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AuthenticationResponse> RegisterUserAsync(RegisterRequest registerRequest)
    {
        if (await _userManager.FindByEmailAsync(registerRequest.Email!) is not null)
        {
            throw new ServiceException("User already exists.", StatusCodes.Status409Conflict);
        }

        var user = registerRequest.ToEntity();

        if (!(await _userManager.CreateAsync(user, registerRequest.Password!)).Succeeded)
        {
            throw new ServiceException("Unable to create user.", StatusCodes.Status500InternalServerError);
        }

        if (!await _roleManager.RoleExistsAsync("User"))
        {
            if (!(await _roleManager.CreateAsync(new IdentityRole<long>("User"))).Succeeded)
            {
                throw new ServiceException("Unable to create role 'User'.", StatusCodes.Status500InternalServerError);
            }
        }

        if (!(await _userManager.AddToRoleAsync(user, "User")).Succeeded)
        {
            throw new ServiceException("Unable to assign user to role 'User'.",
                StatusCodes.Status500InternalServerError);
        }

        var accessToken = JwtUtilities.CreateAccessToken(
            _jwtTokenInfo,
            new JwtClaimInfoDTO(user.Id, user.DisplayName, ["User"])
        );
        var refreshToken = JwtUtilities.CreateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryDate = DateTime.UtcNow.AddHours(2);
        if (!(await _userManager.UpdateAsync(user)).Succeeded)
        {
            throw new ServiceException("Unable to set refresh token.", StatusCodes.Status500InternalServerError);
        }

        AddCookie(JwtUtilities.TokenType.AccessToken, accessToken);
        AddCookie(JwtUtilities.TokenType.RefreshToken, refreshToken);

        return new AuthenticationResponse(user.ToResponse(), accessToken, refreshToken);
    }

    public async Task<AuthenticationResponse> LoginUserAsync(LoginRequest loginRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginRequest.Email!);
        if (user is null)
        {
            throw new ServiceException("User not found.", StatusCodes.Status404NotFound);
        }

        var authenticated = await _userManager.CheckPasswordAsync(user, loginRequest.Password!);
        if (!authenticated)
        {
            throw new ServiceException("Invalid credentials.", StatusCodes.Status401Unauthorized);
        }

        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = JwtUtilities.CreateAccessToken(
            _jwtTokenInfo,
            new JwtClaimInfoDTO(user.Id, user.DisplayName, roles)
        );
        var refreshToken = JwtUtilities.CreateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryDate = DateTime.UtcNow.AddHours(2);
        if (!(await _userManager.UpdateAsync(user)).Succeeded)
        {
            throw new ServiceException("Unable to set refresh token.", StatusCodes.Status500InternalServerError);
        }

        AddCookie(JwtUtilities.TokenType.AccessToken, accessToken);
        AddCookie(JwtUtilities.TokenType.RefreshToken, refreshToken);

        return new AuthenticationResponse(user.ToResponse(), accessToken, refreshToken);
    }

    public async Task<AuthenticationResponse> RefreshTokensAsync(string? accessToken, string? refreshToken)
    {
        if (accessToken is null)
        {
            throw new ServiceException("Cannot find user.", StatusCodes.Status400BadRequest);
        }

        if (refreshToken is null)
        {
            throw new ServiceException("Refresh token is required.", StatusCodes.Status401Unauthorized);
        }

        var userId = JwtUtilities.GetUserIdFromExpiredAccessToken(_jwtTokenInfo, accessToken);
        if (userId is null)
        {
            throw new ServiceException("Cannot find user.", StatusCodes.Status400BadRequest);
        }

        var user = await _userManager.FindByIdAsync(userId);
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
        var newAccessToken = JwtUtilities.CreateAccessToken(
            _jwtTokenInfo,
            new JwtClaimInfoDTO(user.Id, user.DisplayName, roles)
        );

        AddCookie(JwtUtilities.TokenType.AccessToken, newAccessToken);

        return new AuthenticationResponse(user.ToResponse(), newAccessToken, refreshToken);
    }

    public async Task LogoutUserAsync(ClaimsPrincipal identity)
    {
        var user = await _userManager.GetUserAsync(identity);
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

        _httpContextAccessor.HttpContext?.Response.Cookies.Delete("AccessToken");
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete("RefreshToken");
    }

    private void AddCookie(JwtUtilities.TokenType tokenType, string token)
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(tokenType.GetString(), token, new CookieOptions
        {
            Expires = DateTime.UtcNow.AddDays(1),
            Path = "/api",
            HttpOnly = true,
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.Strict,
        });
    }
}