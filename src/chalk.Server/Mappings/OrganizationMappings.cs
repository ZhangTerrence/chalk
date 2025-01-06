using System.Globalization;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;
using chalk.Server.Shared;

namespace chalk.Server.Mappings;

public static class OrganizationMappings
{
    public static Organization ToEntity(this CreateOrganizationRequest request, User owner)
    {
        return new Organization
        {
            Name = request.Name!,
            Description = request.Description,
            ProfilePicture = request.ProfilePicture,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Owner = owner
        };
    }

    public static OrganizationResponse ToDTO(this Organization organization)
    {
        return new OrganizationResponse(
            organization.Id,
            organization.Name,
            organization.Description,
            organization.ProfilePicture,
            organization.CreatedDate.ToString(CultureInfo.CurrentCulture),
            organization.UpdatedDate.ToString(CultureInfo.CurrentCulture),
            new UserDTO(organization.Owner, organization.CreatedDate.ToString(CultureInfo.CurrentCulture)),
            organization.Users
                .Select(e => new UserDTO(e.User, e.JoinedDate?.ToString(CultureInfo.CurrentCulture)))
                .ToList(),
            organization.Roles
                .Select(e => new OrganizationRoleDTO(e))
                .ToList(),
            organization.Channels
                .Select(e => new ChannelDTO(e))
                .ToList(),
            organization.Courses
                .Select(e => new CourseDTO(e))
                .ToList()
        );
    }

    public static UserOrganization ToEntity(
        UserStatus userStatus,
        Organization organization,
        User user,
        OrganizationRole organizationRole
    )
    {
        return new UserOrganization
        {
            UserStatus = userStatus,
            Organization = organization,
            User = user,
            Role = organizationRole,
        };
    }

    public static OrganizationRole ToEntity(this CreateOrganizationRoleRequest request)
    {
        return new OrganizationRole
        {
            Name = request.Name!,
            Description = request.Description,
            Permissions = request.Permissions!.Value,
            Rank = request.Rank!.Value,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
    }

    public static RoleResponse ToDTO(this OrganizationRole organizationRole)
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
}