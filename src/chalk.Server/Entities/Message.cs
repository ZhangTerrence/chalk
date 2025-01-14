namespace chalk.Server.Entities;

/// <summary>
/// Represents a user message within a channel.
/// </summary>
public class Message
{
    // Properties
    public long Id { get; set; }
    public required string Text { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    // Foreign Keys
    public long UserId { get; set; }
    public long ChannelId { get; set; }
}