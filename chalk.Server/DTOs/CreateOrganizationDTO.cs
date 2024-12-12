namespace chalk.Server.DTOs;

public record CreateOrganizationDTO(string Name, string Description, long OwnerId)
{
}