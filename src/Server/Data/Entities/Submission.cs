namespace Server.Data.Entities;

/// <summary>
/// Represents a user submission to an assignment.
/// </summary>
public class Submission
{
  // Properties
  public long Id { get; init; }
  public int? Grade { get; set; }
  public string? Feedback { get; set; }
  public required DateTime CreatedOnUtc { get; init; }
  public required DateTime UpdatedOnUtc { get; set; }

  // Foreign Keys
  public long UserId { get; init; }
  public long AssignmentId { get; init; }

  // Navigation Properties
  public ICollection<File> Files { get; init; } = [];
}