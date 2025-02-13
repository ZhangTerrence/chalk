using Server.Common.Enums;

namespace Server.Data.Entities;

/// <summary>
/// Represents the relationship between a user and a course.
/// </summary>
public class UserCourse
{
  /// <summary>
  /// The user's status in the course.
  /// </summary>
  public UserStatus Status { get; set; }

  /// <summary>
  /// The user's grade in the course.
  /// </summary>
  public int? Grade { get; set; }

  /// <summary>
  /// The user's join date in the course.
  /// </summary>
  public DateTime? JoinedOnUtc { get; set; }

  /// <summary>
  /// The id of the specific <see cref="User" />.
  /// </summary>
  public long UserId { get; init; }

  /// <summary>
  /// The id of the specific <see cref="Course" />.
  /// </summary>
  public long CourseId { get; init; }

  /// <summary>
  /// The specific <see cref="User" />.
  /// </summary>
  public User User { get; init; } = null!;

  /// <summary>
  /// The specific <see cref="Course" />.
  /// </summary>
  public Course Course { get; init; } = null!;

  /// <summary>
  /// The user's roles in the course. See <see cref="UserRole" /> for more details.
  /// </summary>
  public ICollection<UserRole> Roles { get; init; } = [];
}
