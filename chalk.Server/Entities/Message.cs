namespace chalk.Server.Entities;

public class Message
{
    public Guid Id { get; set; }
    public required string Text { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    public Guid ChannelId { get; set; }
    public Guid UserId { get; set; }

    public Channel Channel { get; set; } = null!;
    public ChannelParticipant ChannelParticipant { get; set; } = null!;
}