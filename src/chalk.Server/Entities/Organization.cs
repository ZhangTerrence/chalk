namespace chalk.Server.Entities;

public class Organization
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Description { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    public long OwnerId { get; set; }

    public User Owner { get; set; } = null!;
    public ICollection<UserOrganization> Users { get; set; } = [];
    public ICollection<OrganizationRole> Roles { get; set; } = [];
    public ICollection<Channel> Channels { get; set; } = [];
    public ICollection<Course> Courses { get; set; } = [];
}