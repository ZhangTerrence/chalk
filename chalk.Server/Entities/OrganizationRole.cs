namespace chalk.Server.Entities;

public class OrganizationRole
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required long Permissions { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public Guid OrganizationId { get; set; }

    public Organization Organization { get; set; } = null!;
    public ICollection<UserOrganization> UserOrganizations { get; set; } = [];
}