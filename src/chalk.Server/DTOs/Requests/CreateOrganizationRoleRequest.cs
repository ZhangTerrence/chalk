namespace chalk.Server.DTOs.Requests;

public record CreateOrganizationRoleRequest(long OrganizationId, string Name, string? Description, long Permissions);