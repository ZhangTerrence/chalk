namespace chalk.Server.Entities;

/// <summary>
/// Represents an organization.
/// </summary>
public class Organization
{
    // Properties
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Description { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    // Foreign Keys
    public long OwnerId { get; set; }

    // Navigation Properties
    public User Owner { get; set; } = null!;
    public ICollection<UserOrganization> Users { get; set; } = [];
    public ICollection<OrganizationRole> Roles { get; set; } = [];
    public ICollection<Channel> Channels { get; set; } = [];
    public ICollection<Course> Courses { get; set; } = [];
    public ICollection<Tag> Tags { get; set; } = [];
}