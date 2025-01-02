namespace chalk.Server.Entities;

public class ChannelUser
{
    public required DateTime JoinedDate { get; set; }

    public long UserId { get; set; }
    public long ChannelId { get; set; }
    public long? OrganizationRoleId { get; set; }
    public long? CourseRoleId { get; set; }

    public User User { get; set; } = null!;
    public Channel Channel { get; set; } = null!;
    public OrganizationRole? OrganizationRole { get; set; }
    public CourseRole? CourseRole { get; set; }
    public ChannelRolePermission? RolePermission { get; set; }
    public ICollection<Message> Messages { get; set; } = [];
}