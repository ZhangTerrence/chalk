namespace chalk.Server.Entities;

public class OrganizationRole
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required long Permissions { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    public long OrganizationId { get; set; }

    public Organization Organization { get; set; } = null!;
    public ICollection<UserOrganization> UserOrganizations { get; set; } = [];
}