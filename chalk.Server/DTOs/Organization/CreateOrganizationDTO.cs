namespace chalk.Server.DTOs.Organization;

public record CreateOrganizationDTO
{
    public CreateOrganizationDTO(string name, string description, Guid ownerId)
    {
        Name = name;
        Description = description;
        OwnerId = ownerId;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public Guid OwnerId { get; set; }
}