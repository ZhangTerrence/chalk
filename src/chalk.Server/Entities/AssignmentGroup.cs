namespace chalk.Server.Entities;

/// <summary>
/// Represents an assignment group within a course.
/// </summary>
public class AssignmentGroup
{
    // Properties
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required int Weight { get; set; }

    // Foreign Keys
    public long CourseId { get; set; }

    // Navigation Properties
    public ICollection<Assignment> Assignments { get; set; } = [];
}