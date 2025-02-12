namespace Server.Data.Entities;

/// <summary>
/// Represents an assignment group within a course.
/// </summary>
public class AssignmentGroup
{
  // Properties
  public long Id { get; init; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public required int Weight { get; set; }
  public required DateTime CreatedOnUtc { get; init; }
  public required DateTime UpdatedOnUtc { get; set; }

  // Foreign Keys
  public long CourseId { get; init; }

  // Navigation Properties
  public ICollection<Assignment> Assignments { get; init; } = [];
}