using chalk.Server.DTOs.Responses;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Route("/api/user"), Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(new ApiResponse<IEnumerable<UserResponse>>(null, users.Select(e => e.ToResponse())));
    }

    [HttpGet("{userId:long}")]
    public async Task<IActionResult> GetUser([FromRoute] long userId)
    {
        var user = await _userService.GetUserAsync(userId);
        return Ok(new ApiResponse<UserResponse>(null, user.ToResponse()));
    }
}