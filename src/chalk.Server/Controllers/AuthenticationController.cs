using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Services.Interfaces;
using chalk.Server.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthenticationController : ControllerBase
{
    // Services
    private readonly IAuthenticationService _authenticationService;

    // Validators
    private readonly IValidator<RegisterRequest> _registerValidator;
    private readonly IValidator<LoginRequest> _loginValidator;

    public AuthenticationController(
        IAuthenticationService authenticationService,
        IValidator<RegisterRequest> registerValidator,
        IValidator<LoginRequest> loginValidator
    )
    {
        _authenticationService = authenticationService;

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

        var response = await _authenticationService.RegisterUserAsync(request);
        return Created(nameof(Register), new ApiResponse<AuthenticationResponse>(null, response));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _loginValidator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return BadRequest(new ApiResponse<object>(result.GetErrorMessages()));
        }

        var response = await _authenticationService.LoginUserAsync(request);
        return Ok(new ApiResponse<AuthenticationResponse>(null, response));
    }

    [HttpPatch("refresh")]
    public async Task<IActionResult> RefreshTokens()
    {
        Request.HttpContext.Items.TryGetValue("AccessToken", out var accessToken);
        Request.HttpContext.Items.TryGetValue("RefreshToken", out var refreshToken);
        var response = await _authenticationService.RefreshTokensAsync(accessToken as string, refreshToken as string);
        return Ok(new ApiResponse<AuthenticationResponse>(null, response));
    }

    [HttpDelete("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _authenticationService.LogoutUserAsync(User);
        return NoContent();
    }
}