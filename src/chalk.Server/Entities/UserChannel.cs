namespace chalk.Server.Entities;

/// <summary>
/// Represents the relationship between a user and a channel.
/// </summary>
public class UserChannel
{
    // Foreign Keys
    public long UserId { get; set; }
    public long ChannelId { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public Channel Channel { get; set; } = null!;
    public ICollection<Message> Messages { get; set; } = [];
}