using chalk.Server.Configurations;
using chalk.Server.Data;
using chalk.Server.DTOs.Requests;
using chalk.Server.Entities;
using chalk.Server.Services.Interfaces;
using chalk.Server.Utilities;
using Microsoft.EntityFrameworkCore;

namespace chalk.Server.Services;

public class UserService : IUserService
{
    private readonly DatabaseContext _context;
    private readonly IFileUploadService _fileUploadService;

    public UserService(DatabaseContext context, IFileUploadService fileUploadService)
    {
        _context = context;
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

    public async Task<User> UpdateUserAsync(long userId, UpdateUserRequest request)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null)
        {
            throw new ServiceException("User not found.", StatusCodes.Status404NotFound);
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.DisplayName = request.DisplayName;
        user.Description = request.Description;
        if (request.ProfilePicture is not null)
        {
            var hash = FileUtilities.S3ObjectHash("user-profile-picture", user.Id.ToString());
            var uri = await _fileUploadService.UploadAsync(hash, request.ProfilePicture);
            user.ProfilePicture = uri;
        }

        user.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return await GetUserAsync(userId);
    }
}