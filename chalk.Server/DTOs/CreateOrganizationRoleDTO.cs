namespace chalk.Server.DTOs;

public record CreateOrganizationRoleDTO(long OrganizationId, string Name, string? Description, long Permissions)
{
}