using System.Globalization;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;
using chalk.Server.Shared;

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
        Status status,
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
            Role = organizationRole,
        };
    }

    public static InviteResponse ToResponse(this UserOrganization userOrganization)
    {
        return new InviteResponse(
            Invite.Organization,
            new OrganizationDTO(userOrganization.Organization.Id, userOrganization.Organization.Name),
            null
        );
    }

    public static OrganizationResponse ToResponse(this Organization organization)
    {
        return new OrganizationResponse(
            organization.Id,
            organization.Name,
            organization.ProfilePicture,
            organization.Description,
            organization.CreatedDate.ToString(CultureInfo.CurrentCulture),
            organization.UpdatedDate.ToString(CultureInfo.CurrentCulture),
            new UserDTO(
                organization.Owner.Id,
                organization.Owner.FirstName,
                organization.Owner.LastName,
                organization.Owner.DisplayName,
                organization.CreatedDate.ToString(CultureInfo.CurrentCulture)
            ),
            organization.Users
                .Select(e => new UserDTO(
                    e.User.Id,
                    e.User.FirstName,
                    e.User.LastName,
                    e.User.DisplayName,
                    e.JoinedDate?.ToString(CultureInfo.CurrentCulture)
                ))
                .ToList(),
            organization.Roles
                .Select(e => new OrganizationRoleDTO(e.Id, e.Name, e.Permissions))
                .ToList(),
            organization.Channels
                .Select(e => new ChannelDTO(e.Id, e.Name))
                .ToList(),
            organization.Courses
                .Select(e => new CourseDTO(e.Id, e.Name, e.Code))
                .ToList()
        );
    }
}