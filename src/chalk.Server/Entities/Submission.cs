namespace chalk.Server.Entities;

/// <summary>
/// Represents a user submission to an assignment.
/// </summary>
public class Submission
{
    // Properties
    public long Id { get; set; }
    public int? Grade { get; set; }
    public string? Feedback { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    // Foreign Keys
    public long UserId { get; set; }
    public long AssignmentId { get; set; }

    // Navigation Properties
    public ICollection<File> Files { get; set; } = [];
}