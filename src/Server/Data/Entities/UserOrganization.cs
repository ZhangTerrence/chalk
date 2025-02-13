using Server.Common.Enums;

namespace Server.Data.Entities;

/// <summary>
/// Represents the relationship between a user and an organization.
/// </summary>
public class UserOrganization
{
  /// <summary>
  /// The user's status in the organization.
  /// </summary>
  public required UserStatus Status { get; set; }

  /// <summary>
  /// The user's join date in the organization.
  /// </summary>
  public DateTime? JoinedOnUtc { get; set; }

  /// <summary>
  /// The id of the specific <see cref="User" />.
  /// </summary>
  public long UserId { get; init; }

  /// <summary>
  /// The id of the specific <see cref="Organization" />.
  /// </summary>
  public long OrganizationId { get; init; }

  /// <summary>
  /// The specific <see cref="User" />.
  /// </summary>
  public User User { get; init; } = null!;

  /// <summary>
  /// The specific <see cref="Organization" />.
  /// </summary>
  public Organization Organization { get; init; } = null!;

  /// <summary>
  /// The user's roles in the organization. See <see cref="UserRole" /> for more details.
  /// </summary>
  public ICollection<UserRole> Roles { get; init; } = [];
}
