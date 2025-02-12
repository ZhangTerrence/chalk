using System.Globalization;
using Server.Common.DTOs;
using Server.Common.Requests.Organization;
using Server.Common.Responses;
using Server.Data.Entities;

namespace Server.Common.Mappings;

public static class OrganizationMappings
{
  public static OrganizationResponse ToResponse(this Organization organization)
  {
    return new OrganizationResponse(
      organization.Id,
      organization.Name,
      organization.Description,
      organization.ImageUrl,
      organization.IsPublic,
      organization.CreatedOnUtc.ToString(CultureInfo.CurrentCulture)
    );
  }

  public static Organization ToEntity(this CreateRequest request, User owner)
  {
    return new Organization
    {
      Name = request.Name,
      Description = request.Description,
      IsPublic = request.IsPublic!.Value,
      CreatedOnUtc = DateTime.UtcNow,
      UpdatedOnUtc = DateTime.UtcNow,
      Owner = owner
    };
  }

  public static OrganizationDto ToDto(this Organization organization)
  {
    return new OrganizationDto(
      organization.Id,
      organization.Name,
      organization.Description,
      organization.ImageUrl,
      organization.IsPublic,
      organization.CreatedOnUtc.ToString(CultureInfo.CurrentCulture)
    );
  }
}
