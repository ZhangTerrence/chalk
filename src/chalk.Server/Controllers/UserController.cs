using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
using chalk.Server.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Route("/api/users"), Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    private readonly IValidator<UpdateUserRequest> _updateUserRequestValidator;

    public UserController(
        IUserService userService,
        IValidator<UpdateUserRequest> updateUserRequestValidator
    )
    {
        _userService = userService;
        _updateUserRequestValidator = updateUserRequestValidator;
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

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromForm] UpdateUserRequest request)
    {
        var validationResult = await _updateUserRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ApiResponse<object>(validationResult.GetErrorMessages()));
        }

        var user = await _userService.UpdateUserAsync(User.GetUserId(), request);
        return Ok(new ApiResponse<UserResponse>(null, user.ToResponse()));
    }

    [HttpPut("{userId:long}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUser([FromRoute] long userId, [FromBody] UpdateUserRequest request)
    {
        var validationResult = await _updateUserRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ApiResponse<object>(validationResult.GetErrorMessages()));
        }

        var user = await _userService.UpdateUserAsync(userId, request);
        return Ok(new ApiResponse<UserResponse>(null, user.ToResponse()));
    }
}