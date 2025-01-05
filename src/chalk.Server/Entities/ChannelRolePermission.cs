namespace chalk.Server.Entities;

/// <summary>
/// Represents a permissions a course or organization role has within a specific channel.
/// </summary>
public class ChannelRolePermission
{
    // Properties
    public required long Permissions { get; set; }

    // Foreign Keys
    public long ChannelId { get; set; }
    public long? CourseRoleId { get; set; }
    public long? OrganizationRoleId { get; set; }

    // Navigation Properties
    public Channel Channel { get; set; } = null!;
    public CourseRole? CourseRole { get; set; }
    public OrganizationRole? OrganizationRole { get; set; }
    public ICollection<ChannelUser> Users { get; set; } = [];
}