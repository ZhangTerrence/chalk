namespace chalk.Server.Entities;

public class Attachment
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Uri { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    public Guid? AssignmentId { get; set; }
    public Guid? SubmissionId { get; set; }
    public Guid? CourseModuleId { get; set; }

    public Assignment? Assignment { get; set; }
    public Submission? Submission { get; set; }
    public CourseModule? CourseModule { get; set; }
}