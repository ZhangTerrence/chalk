using chalk.Server.DTOs;
using chalk.Server.Extensions;
using chalk.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public UserController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>(ModelState));
        }

        var user = await _userService.RegisterUserAsync(registerRequestDTO);
        var accessToken = _tokenService.CreateAccessToken(user.DisplayName, ["User"]);
        var refreshToken = _tokenService.CreateRefreshToken();

        HttpContext.Response.Cookies.Append("AccessToken", accessToken, new CookieOptions
        {
            Expires = DateTime.Now.AddHours(1).ToUniversalTime(),
            Secure = true,
            SameSite = SameSiteMode.Strict,
            HttpOnly = true,
            IsEssential = true
        });
        HttpContext.Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
        {
            Expires = DateTime.Now.AddDays(1).ToUniversalTime(),
            Secure = true,
            SameSite = SameSiteMode.Strict,
            HttpOnly = true,
            IsEssential = true
        });

        await _userService.AddRefreshTokenAsync(user, refreshToken);

        return Ok(new ApiResponseDTO<UserResponseDTO>(user.ToUserResponseDTO()));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>(ModelState));
        }

        var (user, roles) = await _userService.AuthenticateUserAsync(loginRequestDTO);
        var accessToken = _tokenService.CreateAccessToken(user.DisplayName, roles);
        var refreshToken = _tokenService.CreateRefreshToken();

        HttpContext.Response.Cookies.Append("AccessToken", accessToken, new CookieOptions
        {
            Expires = DateTime.Now.AddHours(1).ToUniversalTime(),
            Secure = true,
            SameSite = SameSiteMode.Strict,
            HttpOnly = true,
            IsEssential = true
        });
        HttpContext.Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
        {
            Expires = DateTime.Now.AddDays(1).ToUniversalTime(),
            Secure = true,
            SameSite = SameSiteMode.Strict,
            HttpOnly = true,
            IsEssential = true
        });

        await _userService.AddRefreshTokenAsync(user, refreshToken);

        return Ok(new ApiResponseDTO<UserResponseDTO>(user.ToUserResponseDTO()));
    }
}