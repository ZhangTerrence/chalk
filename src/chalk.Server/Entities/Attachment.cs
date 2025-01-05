namespace chalk.Server.Entities;

/// <summary>
/// Represents an attachment to either a course assignment, submission, or module.
/// </summary>
public class Attachment
{
    // Properties
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Resource { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    // Foreign Keys
    public long? AssignmentId { get; set; }
    public long? SubmissionId { get; set; }
    public long? CourseModuleId { get; set; }

    // Navigation Properties
    public Assignment? Assignment { get; set; }
    public Submission? Submission { get; set; }
    public CourseModule? CourseModule { get; set; }
}