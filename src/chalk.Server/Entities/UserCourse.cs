using chalk.Server.Shared;

namespace chalk.Server.Entities;

public class UserCourse
{
    public Status Status { get; set; }
    public DateTime? JoinedDate { get; set; }

    public long UserId { get; set; }
    public long CourseId { get; set; }
    public long CourseRoleId { get; set; }

    public User User { get; set; } = null!;
    public Course Course { get; set; } = null!;
    public CourseRole CourseRole { get; set; } = null!;
}