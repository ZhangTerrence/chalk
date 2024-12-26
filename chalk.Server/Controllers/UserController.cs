using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Services.Interfaces;
using FluentValidation;
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
    private readonly IValidator<RespondToInviteRequest> _respondToInviteValidator;

    public UserController(IUserService userService, IValidator<RespondToInviteRequest> respondToInviteValidator)
    {
        _userService = userService;

        _respondToInviteValidator = respondToInviteValidator;
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

    [HttpGet("invite/{id:long}")]
    public async Task<IActionResult> GetInvites([FromRoute] long id)
    {
        var invites = await _userService.GetPendingInvitesAsync(User, id);
        return Ok(new ApiResponse<IEnumerable<InviteResponse>>(null, invites));
    }

    [HttpPost("invite")]
    public async Task<IActionResult> RespondInvite([FromBody] RespondToInviteRequest respondToInviteRequest)
    {
        var result = await _respondToInviteValidator.ValidateAsync(respondToInviteRequest);
        if (!result.IsValid)
        {
            return BadRequest(new ApiResponse<object>(result.Errors.Select(e => e.ErrorMessage), null));
        }

        await _userService.RespondToInviteAsync(respondToInviteRequest);
        return NoContent();
    }
}