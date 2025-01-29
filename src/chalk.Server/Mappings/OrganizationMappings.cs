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
            Name = request.Name,
            Description = request.Description,
            IsPublic = request.IsPublic!.Value,
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
            organization.ImageUrl,
            organization.IsPublic,
            organization.CreatedDate.ToString(CultureInfo.CurrentCulture),
            organization.Owner.ToDTO(organization.CreatedDate.ToString(CultureInfo.CurrentCulture)),
            organization.Users.Select(e => e.User.ToDTO(e.JoinedDate?.ToString(CultureInfo.CurrentCulture))),
            organization.Roles.Select(e => e.ToDTO()),
            organization.Channels.Select(e => e.ToDTO()),
            organization.Courses.Select(e => e.ToDTO())
        );
    }

    public static OrganizationDTO ToDTO(this Organization organization)
    {
        return new OrganizationDTO(
            organization.Id,
            organization.Name,
            organization.Description,
            organization.ImageUrl,
            organization.IsPublic,
            organization.CreatedDate.ToString(CultureInfo.CurrentCulture)
        );
    }
}