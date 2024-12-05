namespace chalk.Server.DTOs.Organization;

public record UpdateOrganizationDTO
{
    public UpdateOrganizationDTO(string? name, string? description, long? ownerId)
    {
        Name = name;
        Description = description;
        OwnerId = ownerId;
    }

    public string? Name { get; }
    public string? Description { get; }
    public long? OwnerId { get; }
}