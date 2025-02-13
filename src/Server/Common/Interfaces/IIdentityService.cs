using Server.Data.Entities;
using LoginRequest = Server.Common.Requests.Identity.LoginRequest;
using RegisterRequest = Server.Common.Requests.Identity.RegisterRequest;

namespace Server.Common.Interfaces;

/// <summary>
/// Interface for Identity services.
/// </summary>
public interface IIdentityService
{
  /// <summary>
  /// Creates a user.
  /// </summary>
  /// <param name="request">The request body. See <see cref="RegisterRequest" /> for more details.</param>
  /// <returns>The email confirmation token.</returns>
  public Task<string> CreateUserAsync(RegisterRequest request);

  /// <summary>
  /// Logins a user.
  /// </summary>
  /// <param name="request">The request body. See <see cref="LoginRequest" /> for more details.</param>
  /// <returns>The authenticated user.</returns>
  public Task<User> LoginUserAsync(LoginRequest request);

  /// <summary>
  /// Refreshes a user's session.
  /// </summary>
  /// <param name="userId">The id of the user.</param>
  /// <returns>The authenticated user.</returns>
  public Task<User> RefreshUserAsync(long userId);

  /// <summary>
  /// Logouts a user.
  /// </summary>
  public Task LogoutUserAsync();

  /// <summary>
  /// Confirms a user's email.
  /// </summary>
  /// <param name="email">The user's email.</param>
  /// <param name="token">The user's email confirmation token.</param>
  public Task ConfirmEmailAsync(string email, string token);
}
