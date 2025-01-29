namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to update a user's profile.
/// </summary>
/// <param name="FirstName">The user's first name.</param>
/// <param name="LastName">The user's last name.</param>
/// <param name="DisplayName">The user's display name.</param>
/// <param name="Description">The user's description.</param>
/// <param name="Image">The user's image.</param>
/// <remarks><paramref name="FirstName"/>, <paramref name="LastName"/>, and <paramref name="DisplayName"/> are required.</remarks>
public record UpdateUserRequest(
    string FirstName,
    string LastName,
    string DisplayName,
    string? Description,
    IFormFile? Image
);