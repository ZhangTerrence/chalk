namespace chalk.Server.DTOs.Organization;

public class UpdateOrganizationDTO
{
    public UpdateOrganizationDTO(string? name, string? description)
    {
        Name = name;
        Description = description;
    }

    public string? Name { get; set; }
    public string? Description { get; set; }
}