namespace chalk.Server.Entities;

/// <summary>
/// Represents a course.
/// </summary>
public class Course
{
    // Properties
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? PreviewImage { get; set; }
    public string? Code { get; set; }
    public required bool Public { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    // Foreign Keys
    public long? OrganizationId { get; set; }

    // Navigation Properties
    public Organization? Organization { get; set; }
    public ICollection<UserCourse> Users { get; set; } = [];
    public ICollection<CourseRole> Roles { get; set; } = [];
    public ICollection<CourseModule> Modules { get; set; } = [];
    public ICollection<AssignmentGroup> AssignmentGroups { get; set; } = [];
    public ICollection<Channel> Channels { get; set; } = [];
    public ICollection<Tag> Tags { get; set; } = [];
}