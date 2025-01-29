namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to create a role for either a course or organization.
/// </summary>
/// <param name="Name">The role's name.</param>
/// <param name="Description">The role's description.</param>
/// <param name="Permissions">The role's permission, stored in bits.</param>
/// <param name="RelativeRank">The role's relative rank compared to other roles in the same course or organization.</param>
/// <remarks><paramref name="Name"/>, <paramref name="Permissions"/>, and <paramref name="RelativeRank"/> are required.</remarks>
public record CreateRoleRequest(
    string Name,
    string? Description,
    long? Permissions,
    int? RelativeRank
);