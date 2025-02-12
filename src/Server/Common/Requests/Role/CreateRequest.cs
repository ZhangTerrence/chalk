namespace Server.Common.Requests.Role;

/// <summary>
/// A request to create a role for either a course or organization.
/// </summary>
/// <param name="Name">The role's name. Required.</param>
/// <param name="Description">The role's description.</param>
/// <param name="Permissions">The role's permission, stored in bits. Required.</param>
/// <param name="RelativeRank">
/// The role's relative rank compared to other roles in the same course or organization.
/// Required.
/// </param>
public record CreateRequest(
  string Name,
  string? Description,
  long? Permissions,
  int? RelativeRank
);