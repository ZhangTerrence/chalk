namespace chalk.Server.Entities;

/// <summary>
/// Represents a file attached to either a module, assignment, or submission.
/// </summary>
public class File
{
    // Properties
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string FileUrl { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    // Foreign Keys
    public long? AssignmentId { get; set; }
    public long? SubmissionId { get; set; }
    public long? CourseModuleId { get; set; }
}