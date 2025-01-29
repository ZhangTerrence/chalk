using chalk.Server.DTOs;
using chalk.Server.DTOs.Requests;
using chalk.Server.Entities;

namespace chalk.Server.Mappings;

public static class RoleMappings
{
    public static Role ToEntity(this CreateRoleRequest request, long? courseId, long? organizationId)
    {
        return new Role
        {
            Name = request.Name,
            Description = request.Description,
            Permissions = request.Permissions!.Value,
            RelativeRank = request.RelativeRank!.Value,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            CourseId = courseId,
            OrganizationId = organizationId
        };
    }

    public static RoleDTO ToDTO(this Role role)
    {
        return new RoleDTO(
            role.Id,
            role.Name,
            role.Description,
            role.Permissions,
            role.RelativeRank
        );
    }
}