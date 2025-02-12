namespace Server.Common.Requests.Organization;

/// <summary>
/// A request to create an organization.
/// </summary>
/// <param name="Name">The organization's name. Required.</param>
/// <param name="Description">The organization's description.</param>
/// <param name="IsPublic">Whether the organization is public. Required.</param>
public record CreateRequest(
  string Name,
  string? Description,
  bool? IsPublic
);