namespace chalk.Server.Entities;

public class ChannelParticipant
{
    public required DateTime JoinedDate { get; set; }

    public long UserId { get; set; }
    public long ChannelId { get; set; }
    public long CourseRoleId { get; set; }

    public User User { get; set; } = null!;
    public Channel Channel { get; set; } = null!;
    public CourseRole CourseRole { get; set; } = null!;
    public ChannelRolePermission? ChannelRolePermission { get; set; }
    public ICollection<Message> Messages { get; set; } = [];
}