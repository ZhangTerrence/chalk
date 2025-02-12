using Server.Common.Enums;

namespace Server.Data.Entities;

/// <summary>
/// Represents the relationship between a user and a course.
/// </summary>
public class UserCourse
{
  // Properties
  public UserStatus Status { get; set; }
  public int? Grade { get; set; }
  public DateTime? JoinedOnUtc { get; set; }

  // Foreign Keys
  public long UserId { get; init; }
  public long CourseId { get; init; }

  // Navigation Properties
  public User User { get; init; } = null!;
  public Course Course { get; init; } = null!;
  public ICollection<UserRole> Roles { get; init; } = [];
}