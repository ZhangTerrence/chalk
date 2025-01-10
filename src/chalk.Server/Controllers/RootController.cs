using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
using chalk.Server.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Route("/api")]
public class RootController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    private readonly IValidator<RegisterRequest> _registerValidator;
    private readonly IValidator<LoginRequest> _loginValidator;

    public RootController(
        IAuthService authService,
        IUserService userService,
        IValidator<RegisterRequest> registerValidator,
        IValidator<LoginRequest> loginValidator
    )
    {
        _authService = authService;
        _userService = userService;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _registerValidator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return BadRequest(new ApiResponse<object>(result.GetErrorMessages()));
        }

        var user = await _userService.CreateUserAsync(request);
        var (_, accessToken, refreshToken) = await _authService.CreateTokensAsync(user, ["User"]);
        return Created(nameof(Register), new ApiResponse<AuthResponse>(null, new AuthResponse(user.ToResponse(), accessToken, refreshToken)));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _loginValidator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return BadRequest(new ApiResponse<object>(result.GetErrorMessages()));
        }

        var (id, accessToken, refreshToken) = await _authService.AuthenticateAsync(request);
        var user = await _userService.GetUserAsync(id);
        return Ok(new ApiResponse<AuthResponse>(null, new AuthResponse(user.ToResponse(), accessToken, refreshToken)));
    }

    [HttpPatch("refresh")]
    public async Task<IActionResult> Refresh()
    {
        Request.HttpContext.Items.TryGetValue("AccessToken", out var accessToken);
        Request.HttpContext.Items.TryGetValue("RefreshToken", out var refreshToken);
        var (id, newAccessToken, sameRefreshToken) = await _authService.RefreshTokenAsync(accessToken as string, refreshToken as string);
        var user = await _userService.GetUserAsync(id);
        return Ok(new ApiResponse<AuthResponse>(null, new AuthResponse(user.ToResponse(), newAccessToken, sameRefreshToken)));
    }

    [HttpDelete("logout"), Authorize]
    public async Task<IActionResult> Logout()
    {
        await _authService.DeleteTokensAsync(User.GetUserId());
        return NoContent();
    }
}