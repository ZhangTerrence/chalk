using Server.Common.Requests.User;
using Server.Data.Entities;

namespace Server.Common.Interfaces;

public interface IUserService
{
  public Task<IEnumerable<User>> GetUsersAsync(long requesterId);

  public Task<User> GetUserByIdAsync(long userId, long requesterId);

  public Task<User> UpdateUserAsync(long userId, long requesterId, UpdateRequest request);
}
