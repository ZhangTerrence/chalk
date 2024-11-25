namespace chalk.Server.Entities;

public class UserCourse
{
    public required DateTime JoinedDate { get; set; }

    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
    public Guid CourseRoleId { get; set; }

    public User User { get; set; } = null!;
    public Course Course { get; set; } = null!;
    public CourseRole CourseRole { get; set; } = null!;
}