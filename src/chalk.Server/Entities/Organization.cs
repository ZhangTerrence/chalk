namespace chalk.Server.Entities;

public class Organization
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? ProfilePictureUri { get; set; }
    public string? Description { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    public long OwnerId { get; set; }

    public User Owner { get; set; } = null!;
    public ICollection<UserOrganization> UserOrganizations { get; set; } = [];
    public ICollection<OrganizationRole> OrganizationRoles { get; set; } = [];
    public ICollection<Course> Courses { get; set; } = [];
}