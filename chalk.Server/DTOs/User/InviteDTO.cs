using chalk.Server.Common;

namespace chalk.Server.DTOs.User;

public record InviteDTO
{
    public InviteDTO(InviteType inviteType, Entities.Organization? organization, Entities.Course? course)
    {
        InviteType = inviteType;
        Organization = organization is not null ? new OrganizationDTO(organization.Id, organization.Name) : null;
        Course = course is not null ? new CourseDTO(course.Id, course.Name, course.Code) : null;
    }

    public InviteType InviteType { get; }
    public OrganizationDTO? Organization { get; }
    public CourseDTO? Course { get; }

    public sealed record OrganizationDTO(long OrganizationId, string Name);

    public sealed record CourseDTO(long CourseId, string Name, string Code);
}