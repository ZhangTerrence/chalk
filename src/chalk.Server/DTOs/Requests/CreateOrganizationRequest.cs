namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to create an organization.
/// </summary>
/// <param name="Name">The organization's name.</param>
/// <param name="Description">The organization's description.</param>
/// <remarks><paramref name="Name"/> is required.</remarks>
public record CreateOrganizationRequest(
    string Name,
    string? Description
);