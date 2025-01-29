namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to update an organization.
/// </summary>
/// <param name="Name">The organization's name.</param>
/// <param name="Description">The organization's description.</param>
/// <param name="Image">The organization's image.</param>
/// <param name="IsPublic">Whether the organization is public.</param>
/// <remarks><paramref name="Name"/> and <paramref name="IsPublic"/> are required.</remarks>
public record UpdateOrganizationRequest(
    string Name,
    string? Description,
    IFormFile? Image,
    bool? IsPublic
);