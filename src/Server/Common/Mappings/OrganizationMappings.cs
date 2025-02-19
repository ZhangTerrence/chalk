using System.Globalization;
using Server.Common.Requests.Organization;
using Server.Common.Responses;
using Server.Data.Entities;

namespace Server.Common.Mappings;

internal static class OrganizationMappings
{
  public static OrganizationResponse ToResponse(this Organization organization)
  {
    return new OrganizationResponse(
      organization.Id,
      organization.Name,
      organization.Description,
      organization.ImageUrl,
      organization.IsPublic,
      organization.CreatedOnUtc.ToString(CultureInfo.CurrentCulture),
      organization.UpdatedOnUtc.ToString(CultureInfo.CurrentCulture)
    );
  }

  public static Organization ToEntity(this CreateRequest request, long ownerId)
  {
    return new Organization
    {
      Name = request.Name,
      Description = request.Description,
      IsPublic = request.IsPublic!.Value,
      CreatedOnUtc = DateTime.UtcNow,
      UpdatedOnUtc = DateTime.UtcNow,
      OwnerId = ownerId
    };
  }
}
