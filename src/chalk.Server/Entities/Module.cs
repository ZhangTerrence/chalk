namespace chalk.Server.Entities;

/// <summary>
/// Represents a module within a course.
/// </summary>
public class Module
{
    // Properties
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required int RelativeOrder { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    // Foreign Keys
    public long CourseId { get; set; }

    // Navigation Properties
    public ICollection<File> Files { get; set; } = [];
}