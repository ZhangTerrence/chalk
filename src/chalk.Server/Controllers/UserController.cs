using chalk.Server.DTOs.Responses;
using chalk.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Route("/api/user")]
[Authorize]
public class UserController : ControllerBase
{
    // Services
    private readonly IUserService _userService;

    // Validators

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(new ApiResponse<IEnumerable<UserResponse>>(null, users));
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetUser([FromRoute] long id)
    {
        var user = await _userService.GetUserAsync(id);
        return Ok(new ApiResponse<UserResponse>(null, user));
    }
}