using chalk.Server.Common;

namespace chalk.Server.DTOs.Organization;

public record OrganizationDTO
{
    public OrganizationDTO(Entities.Organization organization)
    {
        Id = organization.Id;
        Name = organization.Name;
        ProfilePicture = organization.ProfilePicture;
        Description = organization.Description;
        OwnerId = organization.OwnerId;
        CreatedDate = organization.CreatedDate;
        UpdatedDate = organization.UpdatedDate;
        Owner = new UserDTO(organization.Owner.Id, organization.Owner.DisplayName, organization.CreatedDate);
        Users = organization.UserOrganizations
            .Where(e => e.Status == MemberStatus.User)
            .Select(e => new UserDTO(e.User.Id, e.User.DisplayName, e.JoinedDate));
        Roles = organization.OrganizationRoles.Select(e => new OrganizationRoleDTO(e.Id, e.Name));
        Courses = organization.Courses.Select(e => new CourseDTO(e.Id, e.Name, e.Code)).ToList();
    }

    public long Id { get; }
    public string Name { get; }
    public string? ProfilePicture { get; }
    public string? Description { get; }
    public long OwnerId { get; }
    public DateTime CreatedDate { get; }
    public DateTime UpdatedDate { get; }
    public UserDTO Owner { get; }
    public IEnumerable<UserDTO> Users { get; }
    public IEnumerable<OrganizationRoleDTO> Roles { get; }
    public IEnumerable<CourseDTO> Courses { get; }

    public sealed record UserDTO(long UserId, string DisplayName, DateTime? JoinedDate);

    public sealed record OrganizationRoleDTO(long RoleId, string Name);

    public sealed record CourseDTO(long CourseId, string Name, string Code);
}