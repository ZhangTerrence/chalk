namespace chalk.Server.Entities;

public class Message
{
    public long Id { get; set; }
    public required string Text { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    public long ChannelId { get; set; }
    public long UserId { get; set; }

    public Channel Channel { get; set; } = null!;
    public ChannelParticipant ChannelParticipant { get; set; } = null!;
}