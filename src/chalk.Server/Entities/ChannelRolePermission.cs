namespace chalk.Server.Entities;

/// <summary>
/// Represents the permissions a course or organization role has within a specific channel.
/// </summary>
public class ChannelRolePermission
{
    // Properties
    public long Id { get; set; }
    public required long Permissions { get; set; }

    // Foreign Keys
    public long ChannelId { get; set; }
    public long? CourseRoleId { get; set; }
    public long? OrganizationRoleId { get; set; }
}