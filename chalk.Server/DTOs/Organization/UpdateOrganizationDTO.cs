namespace chalk.Server.DTOs.Organization;

public class UpdateOrganizationDTO
{
    public UpdateOrganizationDTO(string? name, string? description, long? ownerId)
    {
        Name = name;
        Description = description;
        OwnerId = ownerId;
    }

    public string? Name { get; set; }
    public string? Description { get; set; }
    public long? OwnerId { get; set; }
}