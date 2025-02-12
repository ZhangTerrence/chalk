namespace Server.Data.Entities;

/// <summary>
/// Represents a file attached to either a module, assignment, or submission.
/// </summary>
public class File
{
  // Properties
  public long Id { get; init; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public required string FileUrl { get; set; }
  public required DateTime CreatedOnUtc { get; init; }
  public required DateTime UpdatedOnUtc { get; set; }

  // Foreign Keys
  public long? AssignmentId { get; set; }
  public long? SubmissionId { get; set; }
  public long? CourseModuleId { get; set; }
}