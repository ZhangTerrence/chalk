namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to update a user's profile.
/// </summary>
/// <param name="FirstName">The user's first name.</param>
/// <param name="LastName">The user's last name.</param>
/// <param name="DisplayName">The user's display name.</param>
/// <param name="Description">The user's description.</param>
/// <param name="ProfilePicture">The user's profile picture.</param>
/// <remarks>All properties are optional.</remarks>
public record UpdateUserRequest(
    string? FirstName,
    string? LastName,
    string? DisplayName,
    string? Description,
    IFormFile? ProfilePicture
);