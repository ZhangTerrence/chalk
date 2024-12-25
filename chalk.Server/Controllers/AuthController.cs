using chalk.Server.DTOs.Requests;
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
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponse<object>(ModelState));
        }

        var response = await _authService.RegisterUserAsync(request);
        return Created(nameof(Register), new ApiResponse<AuthResponse>(null, response));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponse<object>(ModelState));
        }

        var response = await _authService.AuthenticateUserAsync(request);
        return Ok(new ApiResponse<AuthResponse>(null, response));
    }

    [HttpDelete("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutUserAsync(User);
        HttpContext.Response.Cookies.Delete("AccessToken");
        HttpContext.Response.Cookies.Delete("RefreshToken");
        return NoContent();
    }

    [HttpPatch("refresh")]
    [Authorize]
    public async Task<IActionResult> RefreshTokens()
    {
        Request.HttpContext.Items.TryGetValue("RefreshToken", out var refreshToken);
        await _authService.RefreshTokensAsync(User, refreshToken as string);
        return NoContent();
    }
}