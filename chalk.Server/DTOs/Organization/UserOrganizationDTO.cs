using chalk.Server.Common;
using chalk.Server.Entities;

namespace chalk.Server.DTOs.Organization;

public record UserOrganizationDTO
{
    public UserOrganizationDTO(UserOrganization userOrganization)
    {
        Status = userOrganization.Status;
        JoinedDate = userOrganization.JoinedDate;
        User = new UserDTO(userOrganization.User.Id, userOrganization.User.DisplayName);
        Organization = new OrganizationDTO(userOrganization.Organization.Id, userOrganization.Organization.Name);
        OrganizationRole =
            new OrganizationRoleDTO(userOrganization.OrganizationRole.Id, userOrganization.OrganizationRole.Name);
    }

    public MemberStatus Status { get; }
    public DateTime? JoinedDate { get; }
    public UserDTO User { get; }
    public OrganizationDTO Organization { get; }
    public OrganizationRoleDTO OrganizationRole { get; }

    public sealed record UserDTO(long UserId, string DisplayName);

    public sealed record OrganizationDTO(long OrganizationId, string Name);

    public sealed record OrganizationRoleDTO(long RoleId, string Name);
}