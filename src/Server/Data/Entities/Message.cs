namespace Server.Data.Entities;

/// <summary>
/// Represents a user message within a channel.
/// </summary>
public class Message
{
  // Properties
  public long Id { get; init; }
  public required string Text { get; set; }
  public required DateTime CreatedOnUtc { get; init; }
  public required DateTime UpdatedOnUtc { get; set; }

  // Foreign Keys
  public long UserId { get; init; }
  public long ChannelId { get; init; }
}