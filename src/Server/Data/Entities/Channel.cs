namespace Server.Data.Entities;

/// <summary>
/// Represents a channel within a course or organization. Can also be used for direct messages between users.
/// </summary>
public class Channel
{
  /// <summary>
  /// The channel's id.
  /// </summary>
  public long Id { get; init; }

  /// <summary>
  /// The channel's name.
  /// </summary>
  public string? Name { get; set; }

  /// <summary>
  /// The channel's description.
  /// </summary>
  public string? Description { get; set; }

  /// <summary>
  /// The channel's creation date.
  /// </summary>
  public required DateTime CreatedOnUtc { get; init; }

  /// <summary>
  /// The channel's update date.
  /// </summary>
  public required DateTime UpdatedOnUtc { get; set; }

  /// <summary>
  /// The id of the <see cref="Course" /> the channel could belong in.
  /// </summary>
  public long? CourseId { get; init; }

  /// <summary>
  /// The id of the <see cref="Organization" /> the channel could belong in.
  /// </summary>
  public long? OrganizationId { get; init; }

  /// <summary>
  /// The channel's users. See <see cref="UserChannel" /> for more details.
  /// </summary>
  public ICollection<UserChannel> Users { get; init; } = [];

  /// <summary>
  /// The channel's message. See <see cref="Message" /> for more details.
  /// </summary>
  public ICollection<Message> Messages { get; init; } = [];

  /// <summary>
  /// The permissions course or organization roles have within the channel. See <see cref="ChannelRolePermission" /> for
  /// more details.
  /// </summary>
  public ICollection<ChannelRolePermission> RolePermissions { get; init; } = [];
}
