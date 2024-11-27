namespace chalk.Server.Entities;

public class ChannelRolePermission
{
    public required long Permissions { get; set; }

    public long ChannelId { get; set; }
    public long CourseRoleId { get; set; }

    public Channel Channel { get; set; } = null!;
    public CourseRole CourseRole { get; set; } = null!;
    public ICollection<ChannelParticipant> ChannelParticipants { get; set; } = [];
}