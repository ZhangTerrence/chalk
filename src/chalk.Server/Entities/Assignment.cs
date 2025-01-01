namespace chalk.Server.Entities;

public class Assignment
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required bool Open { get; set; }
    public int? MaxGrade { get; set; }
    public DateTime? DueDate { get; set; }
    public int? AllowedAttempts { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    public long CourseId { get; set; }

    public Course Course { get; set; } = null!;
    public ICollection<Attachment> Attachments { get; set; } = [];
    public ICollection<Submission> Submissions { get; set; } = [];
}