using System.Security.Claims;
using chalk.Server.DTOs;
using chalk.Server.DTOs.User;
using chalk.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Route("/api/[controller]")]
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

        var (createdUser, accessToken, refreshToken) = await _userService.RegisterUserAsync(registerDTO);

        HttpContext.Response.Cookies.Append("AccessToken", accessToken, new CookieOptions
        {
            Expires = DateTime.Now.AddHours(1).ToUniversalTime(),
            Secure = true,
            SameSite = SameSiteMode.Strict,
            HttpOnly = true,
            IsEssential = true
        });
        HttpContext.Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
        {
            Expires = DateTime.Now.AddDays(1).ToUniversalTime(),
            Secure = true,
            SameSite = SameSiteMode.Strict,
            HttpOnly = true,
            IsEssential = true
        });

        return Ok(new ApiResponseDTO<UserDTO>(createdUser));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>(ModelState));
        }

        var (user, accessToken, refreshToken) = await _userService.AuthenticateUserAsync(loginDTO);

        HttpContext.Response.Cookies.Append("AccessToken", accessToken, new CookieOptions
        {
            Expires = DateTime.Now.AddHours(1).ToUniversalTime(),
            Secure = true,
            SameSite = SameSiteMode.Strict,
            HttpOnly = true,
            IsEssential = true
        });
        HttpContext.Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
        {
            Expires = DateTime.Now.AddDays(1).ToUniversalTime(),
            Secure = true,
            SameSite = SameSiteMode.Strict,
            HttpOnly = true,
            IsEssential = true
        });

        return Ok(new ApiResponseDTO<UserDTO>(user));
    }

    [HttpDelete("logout")]
    [Authorize(Roles = "User,Admin")]
    public IActionResult Logout()
    {
        HttpContext.Response.Cookies.Delete("AccessToken");
        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(new ApiResponseDTO<IEnumerable<UserDTO>>(users));
    }

    [HttpGet("{userId:long}")]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> GetUser([FromRoute] long userId)
    {
        var user = await _userService.GetUserAsync(userId);
        return Ok(new ApiResponseDTO<UserDTO>(user));
    }

    [HttpGet("invite")]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> GetInvites()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var invites = await _userService.GetPendingInvitesAsync(long.Parse(userId));
        return Ok(new ApiResponseDTO<IEnumerable<InviteDTO>>(invites));
    }

    [HttpPost("invite")]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> RespondInvite([FromBody] InviteResponseDTO inviteResponseDTO)
    {
        if (ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>(ModelState));
        }

        await _userService.RespondInviteAsync(inviteResponseDTO);
        return NoContent();
    }
}