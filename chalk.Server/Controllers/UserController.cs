using chalk.Server.DTOs;
using chalk.Server.DTOs.User;
using chalk.Server.Services.Interfaces;
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
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>(ModelState));
        }

        var createdUser = await _userService.RegisterUserAsync(registerDTO);
        var accessToken = _tokenService.CreateAccessToken(createdUser.DisplayName, ["User"]);
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

        await _userService.AddRefreshTokenAsync(createdUser.Id, refreshToken);

        return Ok(new ApiResponseDTO<UserDTO>(createdUser));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>(ModelState));
        }

        var (user, roles) = await _userService.AuthenticateUserAsync(loginDTO);
        var accessToken = _tokenService.CreateAccessToken(user.Email, roles);
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

        await _userService.AddRefreshTokenAsync(user.Id, refreshToken);

        return Ok(new ApiResponseDTO<UserDTO>(user));
    }

    [HttpDelete("logout")]
    [Authorize(Roles = "User,Admin")]
    public IActionResult Logout()
    {
        HttpContext.Response.Cookies.Delete("AccessToken");
        return Ok();
    }
}