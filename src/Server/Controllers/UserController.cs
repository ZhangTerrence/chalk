using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.User;
using Server.Common.Responses;
using Server.Common.Utilities;
using Server.Infrastructure.Filters;

namespace Server.Controllers;

[ApiController]
[Authorize]
[Route("/api/users")]
public class UserController : ControllerBase
{
  private readonly IUserService _userService;

  public UserController(IUserService userService)
  {
    this._userService = userService;
  }

  [HttpGet]
  public async Task<IActionResult> GetUsers()
  {
    var users = await this._userService.GetUsersAsync(this.User.GetUserId());
    return this.Ok(new Response<IEnumerable<UserResponse>>(null, users.Select(e => e.ToResponse())));
  }

  [HttpGet("{userId:long}")]
  public async Task<IActionResult> GetUser([FromRoute] long userId)
  {
    var user = await this._userService.GetUserByIdAsync(userId, this.User.GetUserId());
    return this.Ok(new Response<UserResponse>(null, user.ToResponse()));
  }

  [HttpPut]
  public async Task<IActionResult> UpdateUser([FromForm] [Validate] UpdateRequest request)
  {
    var user = await this._userService.UpdateUserAsync(this.User.GetUserId(), this.User.GetUserId(), request);
    return this.Ok(new Response<UserResponse>(null, user.ToResponse()));
  }

  [Authorize(Roles = "Admin")]
  [HttpPut("{userId:long}")]
  public async Task<IActionResult> UpdateUser([FromRoute] long userId, [FromBody] [Validate] UpdateRequest request)
  {
    var user = await this._userService.UpdateUserAsync(userId, this.User.GetUserId(), request);
    return this.Ok(new Response<UserResponse>(null, user.ToResponse()));
  }
}
