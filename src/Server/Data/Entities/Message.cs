namespace Server.Data.Entities;

/// <summary>
/// Represents a user message within a channel.
/// </summary>
public class Message
{
  /// <summary>
  /// The message's id.
  /// </summary>
  public long Id { get; init; }

  /// <summary>
  /// The message's text.
  /// </summary>
  public required string Text { get; set; }

  /// <summary>
  /// The message's creation date.
  /// </summary>
  public required DateTime CreatedOnUtc { get; init; }

  /// <summary>
  /// The message's update date.
  /// </summary>
  public required DateTime UpdatedOnUtc { get; set; }

  /// <summary>
  /// The id of the <see cref="User" /> who wrote the message.
  /// </summary>
  public long UserId { get; init; }

  /// <summary>
  /// The id of the <see cref="Channel" /> the message belongs in.
  /// </summary>
  public long ChannelId { get; init; }
}
