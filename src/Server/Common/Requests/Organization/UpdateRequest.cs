namespace Server.Common.Requests.Organization;

/// <summary>
/// A request to update an organization.
/// </summary>
/// <param name="Name">The organization's name. Required.</param>
/// <param name="Description">The organization's description.</param>
/// <param name="Image">The organization's image.</param>
/// <param name="IsPublic">Whether the organization is public. Required.</param>
public record UpdateRequest(
  string Name,
  string? Description,
  IFormFile? Image,
  bool? IsPublic
);