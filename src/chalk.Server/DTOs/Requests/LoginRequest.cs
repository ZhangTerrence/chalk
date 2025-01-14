namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to log in to an account.
/// </summary>
/// <param name="Email">The user's email.</param>
/// <param name="Password">The user's password.</param>
/// <remarks>All properties are required.</remarks>
public record LoginRequest(
    string Email,
    string Password
);