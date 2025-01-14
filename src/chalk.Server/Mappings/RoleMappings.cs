using chalk.Server.DTOs;
using chalk.Server.DTOs.Requests;
using chalk.Server.Entities;

namespace chalk.Server.Mappings;

public static class RoleMappings
{
    public static Role ToEntity(this CreateRoleRequest request)
    {
        return new Role
        {
            Name = request.Name,
            Description = request.Description,
            Permissions = request.Permissions!.Value,
            RelativeRank = request.RelativeRank!.Value,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
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