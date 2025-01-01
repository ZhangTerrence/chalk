namespace chalk.Server.Entities;

public class Course
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    public long OrganizationId { get; set; }

    public Organization Organization { get; set; } = null!;
    public ICollection<UserCourse> UserCourses { get; set; } = [];
    public ICollection<CourseModule> CourseModules { get; set; } = [];
    public ICollection<Assignment> Assignments { get; set; } = [];
    public ICollection<CourseRole> CourseRoles { get; set; } = [];
    public ICollection<Channel> Channels { get; set; } = [];
}