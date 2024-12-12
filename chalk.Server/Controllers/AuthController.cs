using chalk.Server.DTOs;
using chalk.Server.DTOs.Responses;
using chalk.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>(ModelState));
        }

        var user = await _authService.RegisterUserAsync(registerDTO);
        return Created(nameof(Register), new ApiResponseDTO<UserResponseDTO>(null, user));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>(ModelState));
        }

        var user = await _authService.AuthenticateUserAsync(loginDTO);
        return Ok(new ApiResponseDTO<UserResponseDTO>(null, user));
    }

    [HttpDelete("logout")]
    [Authorize(Roles = "User,Admin")]
    public IActionResult Logout()
    {
        HttpContext.Response.Cookies.Delete("AccessToken");
        HttpContext.Response.Cookies.Delete("RefreshToken");
        return NoContent();
    }

    [HttpPost("refresh")]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> RefreshTokens()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>(ModelState));
        }

        Request.HttpContext.Items.TryGetValue("RefreshToken", out var refreshToken);
        await _authService.RefreshTokenAsync(User, refreshToken as string);
        return NoContent();
    }
}