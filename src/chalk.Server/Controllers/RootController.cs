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

    private readonly IValidator<RegisterRequest> _registerRequestValidator;
    private readonly IValidator<LoginRequest> _loginRequestValidator;

    public RootController(
        IAuthService authService,
        IUserService userService,
        IValidator<RegisterRequest> registerRequestValidator,
        IValidator<LoginRequest> loginRequestValidator
    )
    {
        _authService = authService;
        _userService = userService;
        _registerRequestValidator = registerRequestValidator;
        _loginRequestValidator = loginRequestValidator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var validationResult = await _registerRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ApiResponse<object>(validationResult.GetErrorMessages()));
        }

        var user = await _userService.CreateUserAsync(request);
        var (_, accessToken, refreshToken) = await _authService.CreateTokensAsync(user, ["User"]);
        var response = new AuthResponse(user.ToResponse(), accessToken, refreshToken);
        return Created(nameof(Register), new ApiResponse<AuthResponse>(null, response));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var validationResult = await _loginRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ApiResponse<object>(validationResult.GetErrorMessages()));
        }

        var (id, accessToken, refreshToken) = await _authService.AuthenticateAsync(request);
        var user = await _userService.GetUserAsync(id);
        var response = new AuthResponse(user.ToResponse(), accessToken, refreshToken);
        return Ok(new ApiResponse<AuthResponse>(null, response));
    }

    [HttpPatch("refresh")]
    public async Task<IActionResult> Refresh()
    {
        if (!Request.HttpContext.Items.TryGetValue("AccessToken", out var accessToken) ||
            !Request.HttpContext.Items.TryGetValue("RefreshToken", out var refreshToken))
        {
            return Unauthorized(new ApiResponse<object>(["Missing tokens."]));
        }

        var (id, newAccessToken, __) = await _authService.RefreshTokenAsync((accessToken as string)!, (refreshToken as string)!);
        var user = await _userService.GetUserAsync(id);
        var response = new AuthResponse(user.ToResponse(), newAccessToken, __);
        return Ok(new ApiResponse<AuthResponse>(null, response));
    }

    [HttpDelete("logout"), Authorize]
    public async Task<IActionResult> Logout()
    {
        await _authService.DeleteTokensAsync(User.GetUserId());
        return NoContent();
    }
}