using System.Globalization;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;

namespace chalk.Server.Mappings;

public static class OrganizationMappings
{
    public static Organization ToEntity(this CreateOrganizationRequest request, User owner)
    {
        return new Organization
        {
            Name = request.Name!,
            Description = request.Description,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Owner = owner
        };
    }

    public static OrganizationResponse ToResponse(this Organization organization)
    {
        return new OrganizationResponse(
            organization.Id,
            organization.Name,
            organization.Description,
            organization.ProfilePicture,
            organization.CreatedDate.ToString(CultureInfo.CurrentCulture),
            organization.UpdatedDate.ToString(CultureInfo.CurrentCulture),
            organization.Owner.ToDTO(organization.CreatedDate.ToString(CultureInfo.CurrentCulture)),
            organization.Users.Select(e => e.User.ToDTO(e.JoinedDate?.ToString(CultureInfo.CurrentCulture))),
            organization.Roles.Select(e => e.ToDTO()),
            organization.Channels.Select(e => e.ToDTO()),
            organization.Courses.Select(e => e.ToDTO())
        );
    }

    public static OrganizationDTO ToDTO(this Organization organization)
    {
        return new OrganizationDTO(organization.Id, organization.Name);
    }

    public static OrganizationRole ToEntity(this CreateRoleRequest request)
    {
        return new OrganizationRole
        {
            Name = request.Name!,
            Description = request.Description,
            Permissions = request.Permissions!.Value,
            Rank = request.Rank!.Value,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };
    }

    public static RoleResponse ToResponse(this OrganizationRole organizationRole)
    {
        return new RoleResponse(
            organizationRole.Id,
            organizationRole.Name,
            organizationRole.Description,
            organizationRole.Permissions,
            organizationRole.Rank,
            organizationRole.CreatedDate.ToString(CultureInfo.CurrentCulture),
            organizationRole.UpdatedDate.ToString(CultureInfo.CurrentCulture)
        );
    }

    public static RoleDTO ToDTO(this OrganizationRole organizationRole)
    {
        return new RoleDTO(organizationRole.Id, organizationRole.Name, organizationRole.Permissions, organizationRole.Rank);
    }
}