using chalk.Server.DTOs;
using chalk.Server.DTOs.Responses;
using chalk.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Route("/api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>(ModelState));
        }

        Console.WriteLine("Hello");
        var authResponseDTO = await _userService.RegisterUserAsync(registerDTO);
        return Created(nameof(Register), new ApiResponseDTO<AuthResponseDTO>(null, authResponseDTO));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>(ModelState));
        }

        var authResponseDTO = await _userService.AuthenticateUserAsync(loginDTO);
        return Ok(new ApiResponseDTO<AuthResponseDTO>(null, authResponseDTO));
    }

    [HttpDelete("logout")]
    [Authorize(Roles = "User,Admin")]
    public IActionResult Logout()
    {
        HttpContext.Response.Cookies.Delete("AccessToken");
        return NoContent();
    }

    [HttpGet]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(new ApiResponseDTO<IEnumerable<UserResponseDTO>>(null, users));
    }

    [HttpGet("{userId:long}")]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> GetUser([FromRoute] long userId)
    {
        var user = await _userService.GetUserAsync(userId);
        return Ok(new ApiResponseDTO<UserResponseDTO>(null, user));
    }

    [HttpGet("invite/{userId:long}")]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> GetInvites([FromRoute] long userId)
    {
        var invites = await _userService.GetPendingInvitesAsync(userId, User);
        return Ok(new ApiResponseDTO<IEnumerable<InviteResponseDTO>>(null, invites));
    }

    [HttpPost("invite")]
    [Authorize(Roles = "User,Admin")]
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