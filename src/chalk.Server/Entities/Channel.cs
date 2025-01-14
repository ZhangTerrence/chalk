namespace chalk.Server.Entities;

/// <summary>
/// Represents a channel within a course or organization. Can also be used for direct messages between users.
/// </summary>
public class Channel
{
    // Properties
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    // Foreign Keys
    public long? CourseId { get; set; }
    public long? OrganizationId { get; set; }

    // Navigation Properties
    public ICollection<UserChannel> Users { get; set; } = [];
    public ICollection<Message> Messages { get; set; } = [];
    public ICollection<ChannelRolePermission> RolePermissions { get; set; } = [];
}