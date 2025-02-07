using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
using chalk.Server.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = chalk.Server.DTOs.Requests.LoginRequest;
using RegisterRequest = chalk.Server.DTOs.Requests.RegisterRequest;

namespace chalk.Server.Controllers;

[ApiController]
[Route("/api/account")]
public class AccountController : ControllerBase
{
    private readonly IConfiguration _configuration;

    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IEmailService _emailService;
    private readonly IUserService _userService;

    private readonly IValidator<RegisterRequest> _registerRequestValidator;
    private readonly IValidator<LoginRequest> _loginRequestValidator;

    public AccountController(
        IConfiguration configuration,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IEmailService emailService,
        IUserService userService,
        IValidator<RegisterRequest> registerRequestValidator,
        IValidator<LoginRequest> loginRequestValidator
    )
    {
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
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
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        var user = request.ToEntity();
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response<object>(result.GetErrorMessages()));
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);
        if (confirmationLink is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response<object>(["Unable to generate confirmation link."]));
        }

        _emailService.SendEmail(request.Email, "Verify your account",
            "Click this link to confirm your email and complete account registrations.\n" +
            confirmationLink + "\n" +
            "This link will expire in 24 hours.");

        return Created();
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token, [FromQuery] string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return NotFound(new Response<object>(["User not found."]));
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new Response<object>(result.GetErrorMessages()));
        }

        await _userManager.AddToRoleAsync(user, "User");

        var webHost = _configuration["Web:Host"];
        var webPort = _configuration["Web:Port"];
        if (webHost is null || webPort is null)
        {
            return NoContent();
        }

        return Redirect(Request.Scheme + "://" + webHost + ":" + webPort + "/login");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var validationResult = await _loginRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return NotFound(new Response<object>(["User not found."]));
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);
        if (result.IsLockedOut)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new Response<object>(["Too many failed attempts, account locked for 5 minutes."]));
        }
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new Response<object>(["Invalid email or password."]));
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        await _signInManager.SignInWithClaimsAsync(user, true, user.CreateClaims(userRoles));
        return Ok(new Response<UserResponse>(null, (await _userService.GetUserAsync(user.Id)).ToResponse()));
    }

    [HttpGet("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var user = await _userManager.FindByIdAsync(User.GetUserId().ToString());
        if (user is null)
        {
            return NotFound(new Response<object>(["User not found."]));
        }

        await _signInManager.RefreshSignInAsync(user);
        return Ok(new Response<UserResponse>(null, (await _userService.GetUserAsync(user.Id)).ToResponse()));
    }

    [HttpDelete("logout"), Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return NoContent();
    }
}