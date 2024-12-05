using chalk.Server.Common;
using chalk.Server.DTOs.Organization;
using chalk.Server.DTOs.User;
using chalk.Server.Entities;

namespace chalk.Server.Extensions;

public static class OrganizationExtensions
{
    public static Organization ToOrganization(this CreateOrganizationDTO createOrganizationDTO, User owner)
    {
        return new Organization
        {
            Name = createOrganizationDTO.Name,
            Description = createOrganizationDTO.Description,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Owner = owner
        };
    }

    public static UserOrganization ToUserOrganization(
        MemberStatus status,
        Organization organization,
        User user,
        OrganizationRole organizationRole
    )
    {
        return new UserOrganization
        {
            Status = status,
            Organization = organization,
            User = user,
            OrganizationRole = organizationRole,
        };
    }

    public static InviteDTO ToInviteDTO(this UserOrganization userOrganization)
    {
        return new InviteDTO(InviteType.Organization, userOrganization.Organization, null);
    }

    public static OrganizationDTO ToOrganizationDTO(this Organization organization)
    {
        return new OrganizationDTO(organization);
    }

    public static UserOrganizationDTO ToUserOrganizationDTO(this UserOrganization userOrganization)
    {
        return new UserOrganizationDTO(userOrganization);
    }
}