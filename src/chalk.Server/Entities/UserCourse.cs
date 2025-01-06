using chalk.Server.Shared;

namespace chalk.Server.Entities;

/// <summary>
/// Represents the relationship between a user and a course, i.e. whether a user is enrolled in a course.
/// </summary>
public class UserCourse
{
    // Properties
    public UserStatus UserStatus { get; set; }
    public required int Grade { get; set; }
    public DateTime? JoinedDate { get; set; }

    // Foreign Keys
    public long UserId { get; set; }
    public long CourseId { get; set; }
    public long CourseRoleId { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public Course Course { get; set; } = null!;
    public CourseRole CourseRole { get; set; } = null!;
}