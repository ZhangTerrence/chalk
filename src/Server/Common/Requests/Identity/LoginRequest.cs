namespace Server.Common.Requests.Identity;

/// <summary>
/// A request to log in to an account.
/// </summary>
/// <param name="Email">The user's email. Required.</param>
/// <param name="Password">The user's password. Required.</param>
public record LoginRequest(
  string Email,
  string Password
);