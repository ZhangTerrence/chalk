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
            organization.CreatedDate.ToString(CultureInfo.CurrentCulture)
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