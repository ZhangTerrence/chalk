using Server.Common.DTOs;
using Server.Common.Requests.Role;
using Server.Data.Entities;

namespace Server.Common.Mappings;

public static class RoleMappings
{
  public static Role ToEntity(this CreateRequest request, long? courseId, long? organizationId)
  {
    return new Role
    {
      Name = request.Name,
      Description = request.Description,
      Permissions = request.Permissions!.Value,
      RelativeRank = request.RelativeRank!.Value,
      CreatedOnUtc = DateTime.UtcNow,
      UpdatedOnUtc = DateTime.UtcNow,
      CourseId = courseId,
      OrganizationId = organizationId
    };
  }

  public static RoleDto ToDto(this Role role)
  {
    return new RoleDto(
      role.Id,
      role.Name,
      role.Description,
      role.Permissions,
      role.RelativeRank
    );
  }
}
