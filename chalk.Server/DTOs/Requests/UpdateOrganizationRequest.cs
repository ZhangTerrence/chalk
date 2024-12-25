namespace chalk.Server.DTOs.Requests;

public record UpdateOrganizationRequest(string? Name, string? Description, long? OwnerId);