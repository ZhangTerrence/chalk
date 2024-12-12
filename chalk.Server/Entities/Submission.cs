namespace chalk.Server.Entities;

public class Submission
{
    public long Id { get; set; }

    public int? Grade { get; set; }
    public string? Feedback { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    public long UserId { get; set; }
    public long AssignmentId { get; set; }

    public User User { get; set; } = null!;
    public Assignment Assignment { get; set; } = null!;
    public ICollection<Attachment> Attachments { get; set; } = [];
}