using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Common.Exceptions;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.Identity;
using Server.Common.Responses;
using Server.Common.Utilities;
using Server.Infrastructure.Filters;

namespace Server.Controllers;

/// <summary>
/// Routes for managing ASP.NET Identity users.
/// </summary>
[ApiController]
[Route("/api")]
[Produces("application/json")]
public class IdentityController : ControllerBase
{
  private readonly IConfiguration _configuration;
  private readonly IEmailService _emailService;
  private readonly IIdentityService _identityService;

  internal IdentityController(IConfiguration configuration, IEmailService emailService,
    IIdentityService identityService)
  {
    this._configuration = configuration;
    this._emailService = emailService;
    this._identityService = identityService;
  }

  /// <summary>
  /// Registers a user.
  /// </summary>
  /// <param name="request">The request body. See <see cref="RegisterRequest" /> for more details.</param>
  [HttpPost("register")]
  [ProducesResponseType(StatusCodes.Status201Created)]
  public async Task<IActionResult> Register([FromBody] [Validate] RegisterRequest request)
  {
    var token = await this._identityService.CreateUserAsync(request);
    var link = this.Url
      .Action(nameof(this.ConfirmEmail), "Identity", new { token, email = request.Email }, this.Request.Scheme);
    if (link is null) ServiceException.InternalServerError("Unable to generate confirmation link.");

    this._emailService.SendEmail(request.Email, "Verify your email",
      "Click this link to confirm your email and complete registration." + "\n" +
      link + "\n" +
      "This link will expire in 24 hours.");

    return this.Created();
  }

  /// <summary>
  /// Confirms a user's email.
  /// </summary>
  /// <param name="token">The user's email confirmation token.</param>
  /// <param name="email">The user's email.</param>
  [HttpGet("confirm-email")]
  [ProducesResponseType(StatusCodes.Status308PermanentRedirect)]
  public async Task<IActionResult> ConfirmEmail([FromQuery] string token, [FromQuery] string email)
  {
    await this._identityService.ConfirmEmailAsync(token, email);

    var webHost = this._configuration["Web:Host"];
    var webPort = this._configuration["Web:Port"];
    if (webHost is null || webPort is null) return this.NoContent();

    return this.RedirectPermanent(this.Request.Scheme + "://" + webHost + ":" + webPort + "/login");
  }

  /// <summary>
  /// Logins a user and creates a new session.
  /// </summary>
  /// <param name="request">The request body. See <see cref="LoginRequest" /> for more details.</param>
  /// <returns>The authenticated user.</returns>
  [HttpPost("login")]
  [ProducesResponseType<Response<UserResponse>>(StatusCodes.Status200OK)]
  public async Task<IActionResult> Login([FromBody] [Validate] LoginRequest request)
  {
    var user = await this._identityService.LoginUserAsync(request);
    return this.Ok(new Response<UserResponse>(null, user.ToResponse()));
  }

  /// <summary>
  /// Refreshes a user's session.
  /// </summary>
  /// <returns>The authenticated user.</returns>
  [HttpGet("refresh")]
  [ProducesResponseType<Response<UserResponse>>(StatusCodes.Status200OK)]
  public async Task<IActionResult> Refresh()
  {
    var user = await this._identityService.RefreshUserAsync(this.User.GetUserId());
    return this.Ok(new Response<UserResponse>(null, user.ToResponse()));
  }

  /// <summary>
  /// Logouts a user.
  /// </summary>
  [Authorize]
  [HttpDelete("logout")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  public async Task<IActionResult> Logout()
  {
    await this._identityService.LogoutUserAsync();
    return this.NoContent();
  }
}
