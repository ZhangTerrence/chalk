namespace Server.Data.Entities;

/// <summary>
/// Represents the relationship between a user and a role.
/// </summary>
public class UserRole
{
  // Foreign Keys
  public long UserId { get; init; }
  public long? CourseId { get; init; }
  public long? OrganizationId { get; init; }
  public long RoleId { get; init; }

  // Navigation Properties
  public UserCourse? UserCourse { get; init; }
  public UserOrganization? UserOrganization { get; init; }
  public Role Role { get; init; } = null!;
}