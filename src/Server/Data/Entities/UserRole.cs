namespace Server.Data.Entities;

/// <summary>
/// Represents the relationship between a user and a role.
/// </summary>
public class UserRole
{
  /// <summary>
  /// The id of the specific <see cref="User" />.
  /// </summary>
  public long UserId { get; init; }

  /// <summary>
  /// The id of the specific <see cref="Course" /> the role could belong in.
  /// </summary>
  public long? CourseId { get; init; }

  /// <summary>
  /// The id of the specific <see cref="Organization" /> the role could belong in.
  /// </summary>
  public long? OrganizationId { get; init; }

  /// <summary>
  /// The id of the specific <see cref="Role" />.
  /// </summary>
  public long RoleId { get; init; }

  /// <summary>
  /// The specific <see cref="Role" />.
  /// </summary>
  public Role Role { get; init; } = null!;
}
