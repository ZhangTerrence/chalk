using chalk.Server.DTOs;
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
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(new ApiResponseDTO<IEnumerable<UserResponseDTO>>(null, users));
    }

    [HttpGet("{userId:long}")]
    public async Task<IActionResult> GetUser([FromRoute] long userId)
    {
        var user = await _userService.GetUserAsync(userId);
        return Ok(new ApiResponseDTO<UserResponseDTO>(null, user));
    }

    [HttpGet("invite/{userId:long}")]
    public async Task<IActionResult> GetInvites([FromRoute] long userId)
    {
        var invites = await _userService.GetPendingInvitesAsync(userId, User);
        return Ok(new ApiResponseDTO<IEnumerable<InviteResponseDTO>>(null, invites));
    }

    [HttpPost("invite")]
    public async Task<IActionResult> RespondInvite([FromBody] RespondToInviteDTO respondToInviteDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>(ModelState));
        }

        await _userService.RespondToInviteAsync(respondToInviteDTO);
        return NoContent();
    }
}