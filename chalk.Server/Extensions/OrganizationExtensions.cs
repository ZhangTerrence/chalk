using System.Globalization;
using chalk.Server.Common;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Responses;
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

    public static InviteResponseDTO ToInviteResponseDTO(this UserOrganization userOrganization)
    {
        return new InviteResponseDTO(
            InviteType.Organization,
            new InviteResponseDTO.OrganizationDTO(userOrganization.Organization.Id, userOrganization.Organization.Name),
            null
        );
    }

    public static OrganizationResponseDTO ToOrganizationResponseDTO(this Organization organization)
    {
        return new OrganizationResponseDTO(
            organization.Id,
            organization.Name,
            organization.ProfilePictureUri,
            organization.Description,
            organization.CreatedDate.ToString(CultureInfo.CurrentCulture),
            organization.UpdatedDate.ToString(CultureInfo.CurrentCulture),
            new OrganizationResponseDTO.UserDTO(
                organization.Owner.Id,
                $"{organization.Owner.FirstName} {organization.Owner.LastName}",
                organization.CreatedDate.ToString(CultureInfo.CurrentCulture)
            ),
            organization.UserOrganizations
                .Select(e => new OrganizationResponseDTO.UserDTO(
                    e.User.Id,
                    e.User.DisplayName,
                    e.JoinedDate?.ToString(CultureInfo.CurrentCulture)
                ))
                .ToList(),
            organization.OrganizationRoles
                .Select(e => new OrganizationResponseDTO.OrganizationRoleDTO(e.Id, e.Name, e.Permissions))
                .ToList(),
            organization.Courses
                .Select(e => new OrganizationResponseDTO.CourseDTO(e.Id, e.Name, e.Code))
                .ToList()
        );
    }
}