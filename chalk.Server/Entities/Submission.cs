namespace chalk.Server.Entities;

public class Submission
{
    public Guid Id { get; set; }
    public int? Grade { get; set; }
    public string? Feedback { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public Guid UserId { get; set; }
    public Guid AssignmentId { get; set; }

    public User User { get; set; } = null!;
    public Assignment Assignment { get; set; } = null!;
    public ICollection<Attachment> Attachments { get; set; } = [];
}