using chalk.Server.Shared;

namespace chalk.Server.Entities;

/// <summary>
/// Represents the relationship between a user and a course.
/// </summary>
public class UserCourse
{
    // Properties
    public UserStatus Status { get; set; }
    public int? Grade { get; set; }
    public DateTime? JoinedDate { get; set; }

    // Foreign Keys
    public long UserId { get; set; }
    public long CourseId { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public Course Course { get; set; } = null!;
    public ICollection<UserRole> Roles { get; set; } = [];
}