using chalk.Server.DTOs.User;
using chalk.Server.Entities;
using chalk.Server.Extensions;
using chalk.Server.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace chalk.Server.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    public UserService(UserManager<User> userManager, RoleManager<IdentityRole<long>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<UserDTO> RegisterUserAsync(RegisterDTO registerDTO)
    {
        if (await _userManager.Users.FirstOrDefaultAsync(e => e.Email == registerDTO.Email)
            is not null)
        {
            throw new BadHttpRequestException("User already exists.", StatusCodes.Status400BadRequest);
        }

        var user = registerDTO.ToUser();

        var createdUser = await _userManager.CreateAsync(user, registerDTO.Password);
        if (!createdUser.Succeeded)
        {
            throw new BadHttpRequestException("Unable to create user.", StatusCodes.Status500InternalServerError);
        }

        if (!await _roleManager.RoleExistsAsync("User"))
        {
            var createdRole = await _roleManager.CreateAsync(new IdentityRole<long>("User"));
            if (!createdRole.Succeeded)
            {
                throw new BadHttpRequestException("Unable to create 'User' role.",
                    StatusCodes.Status500InternalServerError);
            }
        }

        var assignedUser = await _userManager.AddToRoleAsync(user, "User");
        if (!assignedUser.Succeeded)
        {
            throw new BadHttpRequestException("Unable to assign user to user role.",
                StatusCodes.Status500InternalServerError);
        }

        return user.ToUserResponseDTO();
    }

    public async Task<(UserDTO, IEnumerable<string>)> AuthenticateUserAsync(LoginDTO loginDTO)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(e => e.Email == loginDTO.Email);
        if (user is null)
        {
            throw new BadHttpRequestException("User not found.", StatusCodes.Status404NotFound);
        }

        var authenticated = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
        if (!authenticated)
        {
            throw new BadHttpRequestException("Invalid email or password.", StatusCodes.Status401Unauthorized);
        }

        var roles = await _userManager.GetRolesAsync(user);

        return (user.ToUserResponseDTO(), roles);
    }

    public async Task AddRefreshTokenAsync(long userId, string refreshToken)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            throw new BadHttpRequestException("User not found.", StatusCodes.Status404NotFound);
        }

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryDate = DateTime.Now.AddDays(1).ToUniversalTime();
        var updatedUser = await _userManager.UpdateAsync(user);
        if (!updatedUser.Succeeded)
        {
            throw new BadHttpRequestException("Unable to update user.", StatusCodes.Status500InternalServerError);
        }
    }
}