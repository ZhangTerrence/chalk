namespace chalk.Server.DTOs;

public record UpdateOrganizationDTO(string? Name, string? Description, long? OwnerId)
{
}