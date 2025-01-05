namespace chalk.Server.Entities;

/// <summary>
/// Represents a module within a course.
/// </summary>
public class CourseModule
{
    // Properties
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required int Order { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    // Foreign Keys
    public long CourseId { get; set; }

    // Navigation Properties
    public Course Course { get; set; } = null!;
    public ICollection<Attachment> Attachments { get; set; } = [];
}