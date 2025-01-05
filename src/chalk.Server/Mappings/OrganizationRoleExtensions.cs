using System.Globalization;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;

namespace chalk.Server.Mappings;

public static class OrganizationRoleExtensions
{
    public static OrganizationRole ToEntity(this CreateOrganizationRoleRequest createOrganizationRoleRequest)
    {
        return new OrganizationRole
        {
            Name = createOrganizationRoleRequest.Name,
            Description = createOrganizationRoleRequest.Description,
            Permissions = createOrganizationRoleRequest.Permissions,
            Rank = 0,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
    }

    public static OrganizationRoleResponse ToResponse(this OrganizationRole organizationRole)
    {
        return new OrganizationRoleResponse(
            organizationRole.Id,
            organizationRole.Name,
            organizationRole.Description,
            organizationRole.Permissions,
            organizationRole.CreatedDate.ToString(CultureInfo.CurrentCulture),
            organizationRole.UpdatedDate.ToString(CultureInfo.CurrentCulture)
        );
    }
}