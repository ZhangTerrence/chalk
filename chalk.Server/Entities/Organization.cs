namespace chalk.Server.Entities;

public class Organization
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public Guid OwnerId { get; set; }

    public User Owner { get; set; } = null!;
    public ICollection<UserOrganization> UserOrganizations { get; set; } = [];
    public ICollection<OrganizationRole> OrganizationRoles { get; set; } = [];
    public ICollection<Course> Courses { get; set; } = [];
}