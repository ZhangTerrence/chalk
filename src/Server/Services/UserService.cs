using Microsoft.EntityFrameworkCore;
using Server.Common.Exceptions;
using Server.Common.Interfaces;
using Server.Common.Requests.User;
using Server.Data;
using Server.Data.Entities;

namespace Server.Services;

internal class UserService : IUserService
{
  private readonly ICloudService _cloudService;
  private readonly DatabaseContext _context;

  public UserService(DatabaseContext context, ICloudService cloudService)
  {
    this._context = context;
    this._cloudService = cloudService;
  }

  public async Task<IEnumerable<User>> GetUsersAsync(long requesterId)
  {
    return await this._context.Users
      .Include(e => e.DirectMessages).ThenInclude(e => e.Channel).AsSplitQuery()
      .Include(e => e.Organizations).ThenInclude(e => e.Organization).AsSplitQuery()
      .Include(e => e.Courses).ThenInclude(e => e.Course).AsSplitQuery()
      .ToListAsync();
  }

  public async Task<User> GetUserByIdAsync(long userId, long requesterId)
  {
    var user = await this._context.Users
      .Include(e => e.DirectMessages).ThenInclude(e => e.Channel).AsSplitQuery()
      .Include(e => e.Organizations).ThenInclude(e => e.Organization).AsSplitQuery()
      .Include(e => e.Courses).ThenInclude(e => e.Course).AsSplitQuery()
      .FirstOrDefaultAsync(e => e.Id == userId);
    if (user is null) ServiceException.NotFound("User not found.", user);

    return user;
  }

  public async Task<User> UpdateUserAsync(long userId, long requesterId, UpdateRequest request)
  {
    var user = await this._context.Users.FindAsync(userId);
    if (user is null) ServiceException.NotFound("User not found.", user);

    user.FirstName = request.FirstName;
    user.LastName = request.LastName;
    user.DisplayName = request.DisplayName;
    user.Description = request.Description;
    if (request.Image is not null)
      user.ImageUrl = await this._cloudService.UploadImageAsync(Guid.NewGuid().ToString(), request.Image);
    user.UpdatedOnUtc = DateTime.UtcNow;

    await this._context.SaveChangesAsync();
    return await this.GetUserByIdAsync(user.Id, requesterId);
  }
}
