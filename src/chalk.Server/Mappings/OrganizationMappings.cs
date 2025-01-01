using System.Globalization;
using chalk.Server.Common;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;

namespace chalk.Server.Mappings;

public static class OrganizationMappings
{
    public static Organization ToEntity(this CreateOrganizationRequest createOrganizationRequest, User owner)
    {
        return new Organization
        {
            Name = createOrganizationRequest.Name,
            Description = createOrganizationRequest.Description,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Owner = owner
        };
    }

    public static UserOrganization ToEntity(
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

    public static InviteResponse ToResponse(this UserOrganization userOrganization)
    {
        return new InviteResponse(
            InviteType.Organization,
            new OrganizationDTO(userOrganization.Organization.Id, userOrganization.Organization.Name),
            null
        );
    }

    public static OrganizationResponse ToResponse(this Organization organization)
    {
        return new OrganizationResponse(
            organization.Id,
            organization.Name,
            organization.ProfilePictureUri,
            organization.Description,
            organization.CreatedDate.ToString(CultureInfo.CurrentCulture),
            organization.UpdatedDate.ToString(CultureInfo.CurrentCulture),
            new UserDTO(
                organization.Owner.Id,
                $"{organization.Owner.FirstName} {organization.Owner.LastName}",
                organization.CreatedDate.ToString(CultureInfo.CurrentCulture)
            ),
            organization.UserOrganizations
                .Select(e => new UserDTO(
                    e.User.Id,
                    e.User.DisplayName,
                    e.JoinedDate?.ToString(CultureInfo.CurrentCulture)
                ))
                .ToList(),
            organization.OrganizationRoles
                .Select(e => new OrganizationRoleDTO(e.Id, e.Name, e.Permissions))
                .ToList(),
            organization.Courses
                .Select(e => new CourseDTO(e.Id, e.Name, e.Code))
                .ToList()
        );
    }
}