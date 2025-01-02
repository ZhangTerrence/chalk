namespace chalk.Server.Entities;

public class ChannelRolePermission
{
    public required long Permissions { get; set; }

    public long ChannelId { get; set; }
    public long? OrganizationRoleId { get; set; }
    public long? CourseRoleId { get; set; }

    public Channel Channel { get; set; } = null!;
    public OrganizationRole? OrganizationRole { get; set; }
    public CourseRole? CourseRole { get; set; }
    public ICollection<ChannelUser> Users { get; set; } = [];
}