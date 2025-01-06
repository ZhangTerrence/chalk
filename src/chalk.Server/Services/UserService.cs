using chalk.Server.Configurations;
using chalk.Server.Data;
using chalk.Server.DTOs.Responses;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace chalk.Server.Services;

public class UserService : IUserService
{
    private readonly DatabaseContext _context;

    public UserService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserResponse>> GetUsersAsync()
    {
        return await _context.Users
            .Include(e => e.DirectMessages).ThenInclude(e => e.Channel).AsSplitQuery()
            .Include(e => e.Organizations).ThenInclude(e => e.Organization).AsSplitQuery()
            .Include(e => e.Courses).ThenInclude(e => e.Course).AsSplitQuery()
            .Select(e => e.ToDTO())
            .ToListAsync();
    }

    public async Task<UserResponse> GetUserAsync(long userId)
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

        return user.ToDTO();
    }
}