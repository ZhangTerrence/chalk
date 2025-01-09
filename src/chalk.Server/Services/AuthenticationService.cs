using chalk.Server.Configurations;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
using chalk.Server.Utilities;
using Microsoft.AspNetCore.Identity;

namespace chalk.Server.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserService _userService;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    private readonly TokenDTO _tokenData;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationService(
        IUserService userService,
        UserManager<User> userManager,
        RoleManager<IdentityRole<long>> roleManager,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _userService = userService;
        _userManager = userManager;
        _roleManager = roleManager;

        _tokenData = new TokenDTO(configuration["Jwt:Issuer"]!, configuration["Jwt:Audience"]!, configuration["Jwt:Key"]!);

        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AuthenticationResponse> RegisterUserAsync(RegisterRequest request)
    {
        if (await _userManager.FindByEmailAsync(request.Email!) is not null)
        {
            throw new ServiceException("User already exists.", StatusCodes.Status409Conflict);
        }

        var user = request.ToEntity();

        if (!(await _userManager.CreateAsync(user, request.Password!)).Succeeded)
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
            throw new ServiceException("Unable to assign user to role 'User'.", StatusCodes.Status500InternalServerError);
        }

        var accessToken = AuthUtilities.CreateAccessToken(_tokenData, new ClaimDTO(user.Id, user.DisplayName, ["User"]));
        var refreshToken = AuthUtilities.CreateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryDate = DateTime.UtcNow.AddHours(2);
        if (!(await _userManager.UpdateAsync(user)).Succeeded)
        {
            throw new ServiceException("Unable to set refresh token.", StatusCodes.Status500InternalServerError);
        }

        AddCookie(AuthUtilities.TokenType.AccessToken, accessToken);
        AddCookie(AuthUtilities.TokenType.RefreshToken, refreshToken);

        return new AuthenticationResponse(await _userService.GetUserAsync(user.Id), accessToken, refreshToken);
    }

    public async Task<AuthenticationResponse> LoginUserAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email!);
        if (user is null)
        {
            throw new ServiceException("User not found.", StatusCodes.Status404NotFound);
        }

        var authenticated = await _userManager.CheckPasswordAsync(user, request.Password!);
        if (!authenticated)
        {
            throw new ServiceException("Invalid credentials.", StatusCodes.Status401Unauthorized);
        }

        var roles = await _userManager.GetRolesAsync(user);
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

        return new AuthenticationResponse(await _userService.GetUserAsync(user.Id), accessToken, refreshToken);
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

        var userId = accessToken.GetUserId(_tokenData);
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
        var newAccessToken = AuthUtilities.CreateAccessToken(_tokenData, new ClaimDTO(user.Id, user.DisplayName, roles));

        AddCookie(AuthUtilities.TokenType.AccessToken, newAccessToken);

        return new AuthenticationResponse(await _userService.GetUserAsync(user.Id), newAccessToken, refreshToken);
    }

    public async Task LogoutUserAsync(long userId)
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

        _httpContextAccessor.HttpContext?.Response.Cookies.Delete("AccessToken");
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete("RefreshToken");
    }

    private void AddCookie(AuthUtilities.TokenType tokenType, string token)
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