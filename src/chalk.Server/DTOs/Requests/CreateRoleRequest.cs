using chalk.Server.Shared;

namespace chalk.Server.DTOs.Requests;

public record CreateRoleRequest(
    Origin? Origin,
    string? Name,
    string? Description,
    long? Permissions,
    int? Rank,
    long? CourseId,
    long? OrganizationId
)
{
    public static CreateRoleRequest CreateCourseRole(string name, string? description, long permissions, int rank, long courseId)
    {
        return new CreateRoleRequest(Shared.Origin.Course, name, description, permissions, rank, courseId, null);
    }

    public static CreateRoleRequest CreateOrganizationRole(
        string name, string? description, long permissions, int rank, long organizationId
    )
    {
        return new CreateRoleRequest(Shared.Origin.Organization, name, description, permissions, rank, null, organizationId);
    }
}