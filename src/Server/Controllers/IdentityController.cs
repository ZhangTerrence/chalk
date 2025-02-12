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

[ApiController]
[Route("/api")]
public class IdentityController : ControllerBase
{
  private readonly IConfiguration _configuration;
  private readonly IEmailService _emailService;
  private readonly IIdentityService _identityService;

  public IdentityController(IConfiguration configuration, IEmailService emailService, IIdentityService identityService)
  {
    this._configuration = configuration;
    this._emailService = emailService;
    this._identityService = identityService;
  }

  [HttpPost("register")]
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

  [HttpGet("confirm-email")]
  public async Task<IActionResult> ConfirmEmail([FromQuery] string token, [FromQuery] string email)
  {
    await this._identityService.ConfirmEmailAsync(token, email);

    var webHost = this._configuration["Web:Host"];
    var webPort = this._configuration["Web:Port"];
    if (webHost is null || webPort is null) return this.NoContent();

    return this.Redirect(this.Request.Scheme + "://" + webHost + ":" + webPort + "/login");
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] [Validate] LoginRequest request)
  {
    var user = await this._identityService.LoginUserAsync(request);
    return this.Ok(new Response<UserResponse>(null, user.ToResponse()));
  }

  [HttpGet("refresh")]
  public async Task<IActionResult> Refresh()
  {
    var user = await this._identityService.RefreshUserAsync(this.User.GetUserId());
    return this.Ok(new Response<UserResponse>(null, user.ToResponse()));
  }

  [Authorize]
  [HttpDelete("logout")]
  public async Task<IActionResult> Logout()
  {
    await this._identityService.LogoutUserAsync();
    return this.NoContent();
  }
}
