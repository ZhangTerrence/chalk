using Server.Common.Requests.User;
using Server.Data.Entities;

namespace Server.Common.Interfaces;

/// <summary>
/// Interface for user services.
/// </summary>
public interface IUserService
{
  /// <summary>
  /// Gets all user.
  /// </summary>
  /// <param name="requesterId">The requester's id.</param>
  /// <returns>A list of all the users.</returns>
  public Task<IEnumerable<User>> GetUsersAsync(long requesterId);

  /// <summary>
  /// Gets a user by id.
  /// </summary>
  /// <param name="userId">The user's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  /// <returns>The user.</returns>
  public Task<User> GetUserByIdAsync(long userId, long requesterId);

  /// <summary>
  /// Updates a user.
  /// </summary>
  /// <param name="userId">The user's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  /// <param name="request">The request body. See <see cref="UpdateRequest" /> for more details.</param>
  /// <returns>The updated user.</returns>
  public Task<User> UpdateUserAsync(long userId, long requesterId, UpdateRequest request);
}
