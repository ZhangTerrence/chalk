namespace Server.Data.Entities;

/// <summary>
/// Represents the relationship between a user and a channel.
/// </summary>
public class UserChannel
{
  // Foreign Keys
  public long UserId { get; init; }
  public long ChannelId { get; init; }

  // Navigation Properties
  public User User { get; init; } = null!;
  public Channel Channel { get; init; } = null!;
  public ICollection<Message> Messages { get; init; } = [];
}