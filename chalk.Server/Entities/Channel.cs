namespace chalk.Server.Entities;

public class Channel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    public Guid? CourseId { get; set; }

    public Course? Course { get; set; }
    public ICollection<ChannelParticipant> ChannelParticipants { get; set; } = [];
    public ICollection<ChannelRolePermission> ChannelRolePermissions { get; set; } = [];
    public ICollection<Message> Messages { get; set; } = [];
}