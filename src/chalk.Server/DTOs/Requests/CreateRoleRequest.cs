using chalk.Server.Shared;

namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to create a role for either a course or organization.
/// </summary>
/// <param name="Origin">Whether it is for a course or an organization.</param>
/// <param name="Name">The role's name.</param>
/// <param name="Description">The role's description.</param>
/// <param name="Permissions">The role's permission, stored in bits.</param>
/// <param name="RelativeRank">The role's relative rank compared to other roles in the same course or organization.</param>
/// <param name="CourseId">The id of the course the role belongs to.</param>
/// <param name="OrganizationId">The id of the organization the role belongs to.</param>
/// <remarks>
///     <paramref name="Origin"/>, <paramref name="Name"/>, <paramref name="Permissions"/>, and <paramref name="RelativeRank"/> are required.
///     <paramref name="CourseId"/> and <paramref name="OrganizationId"/> are conditionally required based on <paramref name="Origin"/>.
/// </remarks>
public record CreateRoleRequest(
    Origin? Origin,
    string Name,
    string? Description,
    long? Permissions,
    int? RelativeRank,
    long? CourseId,
    long? OrganizationId
)
{
    public static CreateRoleRequest CreateCourseRole(string name, string? description, long permissions, int rank, long courseId)
    {
        return new CreateRoleRequest(Shared.Origin.Course, name, description, permissions, rank, courseId, null);
    }

    public static CreateRoleRequest CreateOrganizationRole(string name, string? description, long permissions, int rank, long organizationId)
    {
        return new CreateRoleRequest(Shared.Origin.Organization, name, description, permissions, rank, null, organizationId);
    }
}