using chalk.Server.Configurations;
using chalk.Server.Data;
using chalk.Server.DTOs.Requests;
using chalk.Server.Entities;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
using chalk.Server.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace chalk.Server.Services;

public class UserService : IUserService
{
    private readonly DatabaseContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    private readonly IFileUploadService _fileUploadService;

    public UserService(DatabaseContext context, UserManager<User> userManager, RoleManager<IdentityRole<long>> roleManager, IFileUploadService fileUploadService)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _fileUploadService = fileUploadService;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users
            .Include(e => e.DirectMessages).ThenInclude(e => e.Channel).AsSplitQuery()
            .Include(e => e.Organizations).ThenInclude(e => e.Organization).AsSplitQuery()
            .Include(e => e.Courses).ThenInclude(e => e.Course).AsSplitQuery()
            .ToListAsync();
    }

    public async Task<User> GetUserAsync(long userId)
    {
        var user = await _context.Users
            .Include(e => e.DirectMessages).ThenInclude(e => e.Channel).AsSplitQuery()
            .Include(e => e.Organizations).ThenInclude(e => e.Organization).AsSplitQuery()
            .Include(e => e.Courses).ThenInclude(e => e.Course).AsSplitQuery()
            .FirstOrDefaultAsync(e => e.Id == userId);
        if (user is null)
        {
            throw new ServiceException("User not found.", StatusCodes.Status404NotFound);
        }

        return user;
    }

    public async Task<User> CreateUserAsync(RegisterRequest request)
    {
        if (await _userManager.FindByEmailAsync(request.Email!) is not null)
        {
            throw new ServiceException("User already exists.", StatusCodes.Status409Conflict);
        }

        var user = request.ToEntity();

        if (!(await _userManager.CreateAsync(user, request.Password!)).Succeeded)
        {
            throw new ServiceException("Unable to create user.", StatusCodes.Status500InternalServerError);
        }

        if (!await _roleManager.RoleExistsAsync("User"))
        {
            if (!(await _roleManager.CreateAsync(new IdentityRole<long>("User"))).Succeeded)
            {
                throw new ServiceException("Unable to create role 'User'.", StatusCodes.Status500InternalServerError);
            }
        }

        if (!(await _userManager.AddToRoleAsync(user, "User")).Succeeded)
        {
            throw new ServiceException("Unable to assign user to role 'User'.", StatusCodes.Status500InternalServerError);
        }

        return user;
    }

    public async Task<User> UpdateUserAsync(long userId, UpdateUserRequest request)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null)
        {
            throw new ServiceException("User not found.", StatusCodes.Status404NotFound);
        }

        if (request.FirstName is not null)
        {
            user.FirstName = request.FirstName;
        }

        if (request.LastName is not null)
        {
            user.LastName = request.LastName;
        }

        if (request.DisplayName is not null)
        {
            user.DisplayName = request.DisplayName;
        }

        if (request.Description is not null)
        {
            user.Description = request.Description;
        }

        if (request.ProfilePicture is not null)
        {
            var hash = FileUtilities.S3ObjectHash("user-profile-picture", user.Id.ToString());
            var uri = await _fileUploadService.UploadAsync(hash, request.ProfilePicture);
            user.ProfilePicture = uri;
        }

        await _context.SaveChangesAsync();
        return await GetUserAsync(userId);
    }
}