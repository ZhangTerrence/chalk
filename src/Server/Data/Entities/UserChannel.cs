namespace Server.Data.Entities;

/// <summary>
/// Represents the relationship between a user and a channel.
/// </summary>
public class UserChannel
{
  /// <summary>
  /// The id of the specific <see cref="User" />.
  /// </summary>
  public long UserId { get; init; }

  /// <summary>
  /// The id of the specific <see cref="Channel" />.
  /// </summary>
  public long ChannelId { get; init; }

  /// <summary>
  /// The specific <see cref="User" />.
  /// </summary>
  public User User { get; init; } = null!;

  /// <summary>
  /// The specific <see cref="Channel" />.
  /// </summary>
  public Channel Channel { get; init; } = null!;

  /// <summary>
  /// The user's messages in the channel. See <see cref="Message" /> for more details.
  /// </summary>
  public ICollection<Message> Messages { get; init; } = [];
}
