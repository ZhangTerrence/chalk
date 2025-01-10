namespace chalk.Server.Entities;

/// <summary>
/// Represents the relationship between a user and a channel.
/// </summary>
public class UserChannel
{
    // Properties
    public required DateTime JoinedDate { get; set; }

    // Foreign Keys
    public long UserId { get; set; }
    public long ChannelId { get; set; }
    public long? CourseRoleId { get; set; }
    public long? OrganizationRoleId { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public Channel Channel { get; set; } = null!;
    public CourseRole? CourseRole { get; set; }
    public OrganizationRole? OrganizationRole { get; set; }
    public ChannelRolePermission? RolePermission { get; set; }
    public ICollection<Message> Messages { get; set; } = [];
}