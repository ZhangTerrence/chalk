namespace chalk.Server.Entities;

public class CourseModule
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    public long CourseId { get; set; }

    public Course Course { get; set; } = null!;
    public ICollection<Attachment> Attachments { get; set; } = [];
}