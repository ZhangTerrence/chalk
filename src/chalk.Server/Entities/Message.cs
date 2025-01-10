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
    public long ChannelId { get; set; }
    public long UserId { get; set; }

    // Navigation Properties
    public Channel Channel { get; set; } = null!;
    public UserChannel User { get; set; } = null!;
}