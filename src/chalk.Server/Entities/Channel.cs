namespace chalk.Server.Entities;

public class Channel
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    public long? OrganizationId { get; set; }
    public long? CourseId { get; set; }

    public Organization? Organization { get; set; }
    public Course? Course { get; set; }
    public ICollection<ChannelUser> Users { get; set; } = [];
    public ICollection<ChannelRolePermission> RolePermissions { get; set; } = [];
    public ICollection<Message> Messages { get; set; } = [];
}