namespace Server.Data.Entities;

/// <summary>
/// Represents the permissions a course or organization role has within a specific channel.
/// </summary>
public class ChannelRolePermission
{
  /// <summary>
  /// The permissions a course or organization role has within the channel.
  /// </summary>
  public required long Permissions { get; set; }

  /// <summary>
  /// The id of the specific <see cref="Channel" />.
  /// </summary>
  public long ChannelId { get; init; }

  /// <summary>
  /// The id of the specific course or organization <see cref="Role" />.
  /// </summary>
  public long RoleId { get; init; }
}
