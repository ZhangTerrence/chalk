using chalk.Server.DTOs.OrganizationRole;
using chalk.Server.Entities;

namespace chalk.Server.Extensions;

public static class OrganizationRoleExtensions
{
    public static OrganizationRole ToOrganizationRole(this CreateOrganizationRoleDTO createOrganizationRoleDTO)
    {
        return new OrganizationRole
        {
            Name = createOrganizationRoleDTO.Name,
            Description = createOrganizationRoleDTO.Description,
            Permissions = createOrganizationRoleDTO.Permissions,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
    }

    public static OrganizationRoleDTO ToOrganizationRoleDTO(this OrganizationRole organizationRole)
    {
        return new OrganizationRoleDTO(organizationRole);
    }
}