namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to register a new account.
/// </summary>
/// <param name="FirstName">The user's first name.</param>
/// <param name="LastName">The user's last name.</param>
/// <param name="DisplayName">The user's display name.</param>
/// <param name="Email">The user's email.</param>
/// <param name="Password">The user's password.</param>
/// <remarks>All properties are required.</remarks>
public record RegisterRequest(
    string FirstName,
    string LastName,
    string DisplayName,
    string Email,
    string Password
);