using Server.Common.Enums;

namespace Server.Data.Entities;

/// <summary>
/// Represents the relationship between a user and an organization.
/// </summary>
public class UserOrganization
{
  // Properties
  public required UserStatus Status { get; set; }
  public DateTime? JoinedOnUtc { get; set; }

  // Foreign Keys
  public long UserId { get; init; }
  public long OrganizationId { get; init; }

  // Navigation Properties
  public User User { get; init; } = null!;
  public Organization Organization { get; init; } = null!;
  public ICollection<UserRole> Roles { get; init; } = [];
}
