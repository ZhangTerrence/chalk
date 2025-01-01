namespace chalk.Server.Entities;

public class Attachment
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string Uri { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    public long? AssignmentId { get; set; }
    public long? SubmissionId { get; set; }
    public long? CourseModuleId { get; set; }

    public Assignment? Assignment { get; set; }
    public Submission? Submission { get; set; }
    public CourseModule? CourseModule { get; set; }
}