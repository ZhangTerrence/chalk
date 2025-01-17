namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to update an organization.
/// </summary>
/// <param name="Name">The organization's name.</param>
/// <param name="Description">The organization's description.</param>
/// <param name="ProfilePicture">The organization's profile picture.</param>
/// <param name="IsPublic">Whether the organization is public.</param>
/// <remarks><paramref name="Name"/> and <paramref name="IsPublic"/> are required.</remarks>
public record UpdateOrganizationRequest(
    string Name,
    string? Description,
    IFormFile? ProfilePicture,
    bool IsPublic
);