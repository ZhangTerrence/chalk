namespace chalk.Server.DTOs.Requests;

public record CreateOrganizationRoleRequest(
    string? Name,
    string? Description,
    long? Permissions,
    int? Rank,
    long? OrganizationId
);