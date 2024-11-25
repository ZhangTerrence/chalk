namespace chalk.Server.Entities;

public class CourseModule
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public Guid CourseId { get; set; }

    public Course Course { get; set; } = null!;
    public ICollection<Attachment> Attachments { get; set; } = [];
}