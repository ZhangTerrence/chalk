namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to update an organization.
/// </summary>
/// <param name="Name">The organization's name.</param>
/// <param name="Description">The organization's description.</param>
/// <param name="ProfilePicture">The organization's profile picture.</param>
/// <remarks>All properties are optional.</remarks>
public record UpdateOrganizationRequest(
    string? Name,
    string? Description,
    IFormFile? ProfilePicture
);