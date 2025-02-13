namespace Server.Common.Requests.Identity;

/// <summary>
/// A request to register a new user.
/// </summary>
/// <param name="FirstName">The user's first name. Required.</param>
/// <param name="LastName">The user's last name. Required.</param>
/// <param name="DisplayName">The user's display name. Required.</param>
/// <param name="Email">The user's email. Required.</param>
/// <param name="Password">The user's password. Required.</param>
public record RegisterRequest(
  string FirstName,
  string LastName,
  string DisplayName,
  string Email,
  string Password
);
