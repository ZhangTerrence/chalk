namespace chalk.Server.Entities;

/// <summary>
/// Represents an assignment within a course.
/// </summary>
public class Assignment
{
    // Properties
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required bool IsOpen { get; set; }
    public DateTime? DueDate { get; set; }
    public int? AllowedAttempts { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    // Foreign Keys
    public long AssignmentGroupId { get; set; }

    // Navigation Properties
    public ICollection<File> Files { get; set; } = [];
    public ICollection<Submission> Submissions { get; set; } = [];
}