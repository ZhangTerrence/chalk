using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.User;
using Server.Common.Responses;
using Server.Common.Utilities;
using Server.Infrastructure.Filters;

namespace Server.Controllers;

/// <summary>
/// Routes for managing users.
/// </summary>
[ApiController] [Authorize]
[Route("/api/users")]
[Produces("application/json")]
public class UserController : ControllerBase
{
  private readonly IUserService _userService;

  internal UserController(IUserService userService)
  {
    this._userService = userService;
  }

  /// <summary>
  /// Gets all users.
  /// </summary>
  /// <returns>A list of the users.</returns>
  [HttpGet]
  public async Task<IActionResult> GetUsers()
  {
    var users = await this._userService.GetUsersAsync(this.User.GetUserId());
    return this.Ok(new Response<IEnumerable<UserResponse>>(null, users.Select(e => e.ToResponse())));
  }

  /// <summary>
  /// Gets a user.
  /// </summary>
  /// <param name="userId">The user's id.</param>
  /// <returns>The user.</returns>
  [HttpGet("{userId:long}")]
  public async Task<IActionResult> GetUser([FromRoute] long userId)
  {
    var user = await this._userService.GetUserByIdAsync(userId, this.User.GetUserId());
    return this.Ok(new Response<UserResponse>(null, user.ToResponse()));
  }

  /// <summary>
  /// Updates a currently authenticated user.
  /// </summary>
  /// <param name="request">The request body. See <see cref="UpdateRequest" /> for more details.</param>
  /// <returns>The updated user.</returns>
  [HttpPut]
  public async Task<IActionResult> UpdateUser([FromForm] [Validate] UpdateRequest request)
  {
    var user = await this._userService.UpdateUserAsync(this.User.GetUserId(), this.User.GetUserId(), request);
    return this.Ok(new Response<UserResponse>(null, user.ToResponse()));
  }
}
