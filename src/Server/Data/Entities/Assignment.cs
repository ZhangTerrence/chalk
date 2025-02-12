namespace Server.Data.Entities;

/// <summary>
/// Represents an assignment within a course.
/// </summary>
public class Assignment
{
  // Properties
  public long Id { get; init; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public DateTime? DueOnUtc { get; set; }
  public required DateTime CreatedOnUtc { get; init; }
  public required DateTime UpdatedOnUtc { get; set; }

  // Foreign Keys
  public long AssignmentGroupId { get; init; }

  // Navigation Properties
  public ICollection<File> Files { get; init; } = [];
  public ICollection<Submission> Submissions { get; init; } = [];
}