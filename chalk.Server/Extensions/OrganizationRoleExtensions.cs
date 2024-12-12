using System.Globalization;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Responses;
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

    public static OrganizationRoleResponseDTO ToOrganizationRoleResponseDTO(this OrganizationRole organizationRole)
    {
        return new OrganizationRoleResponseDTO(
            organizationRole.Id,
            organizationRole.Name,
            organizationRole.Description,
            organizationRole.Permissions,
            organizationRole.CreatedDate.ToString(CultureInfo.CurrentCulture),
            organizationRole.UpdatedDate.ToString(CultureInfo.CurrentCulture)
        );
    }
}