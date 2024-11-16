using chalk.Server.DTOs;
using chalk.Server.Entities;
using chalk.Server.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace chalk.Server.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public UserService(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<User> RegisterUserAsync(RegisterRequestDTO registerRequestDTO)
    {
        if (await _userManager.Users.FirstOrDefaultAsync(e => e.Email == registerRequestDTO.Email)
            is not null)
        {
            throw new BadHttpRequestException("User already exists.", StatusCodes.Status400BadRequest);
        }

        var user = registerRequestDTO.ToUser();

        var createdUser = await _userManager.CreateAsync(user, registerRequestDTO.Password);
        if (!createdUser.Succeeded)
        {
            throw new BadHttpRequestException("Unable to create user.", StatusCodes.Status500InternalServerError);
        }

        if (!await _roleManager.RoleExistsAsync("User"))
        {
            var createdRole = await _roleManager.CreateAsync(new IdentityRole<Guid>("User"));
            if (!createdRole.Succeeded)
            {
                throw new BadHttpRequestException("Unable to create user role.",
                    StatusCodes.Status500InternalServerError);
            }
        }

        var assignedUser = await _userManager.AddToRoleAsync(user, "User");
        if (!assignedUser.Succeeded)
        {
            throw new BadHttpRequestException("Unable to assign user to user role.",
                StatusCodes.Status500InternalServerError);
        }

        return user;
    }

    public async Task<(User, IEnumerable<string>)> AuthenticateUserAsync(LoginRequestDTO loginRequestDTO)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(e => e.Email == loginRequestDTO.Email);
        if (user is null)
        {
            throw new BadHttpRequestException("User not found.", StatusCodes.Status404NotFound);
        }

        var authenticated = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
        if (!authenticated)
        {
            throw new BadHttpRequestException("Invalid email or password.", StatusCodes.Status401Unauthorized);
        }

        var roles = await _userManager.GetRolesAsync(user);

        return (user, roles);
    }

    public async Task AddRefreshTokenAsync(User user, string refreshToken)
    {
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryDate = DateTime.Now.AddDays(1).ToUniversalTime();
        var updatedUser = await _userManager.UpdateAsync(user);
        if (!updatedUser.Succeeded)
        {
            throw new BadHttpRequestException("Unable to update user.", StatusCodes.Status500InternalServerError);
        }
    }
}