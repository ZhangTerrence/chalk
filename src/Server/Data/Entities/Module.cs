namespace Server.Data.Entities;

/// <summary>
/// Represents a module within a course.
/// </summary>
public class Module
{
  // Properties
  public long Id { get; init; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public required int RelativeOrder { get; set; }
  public required DateTime CreatedOnUtc { get; init; }
  public required DateTime UpdatedOnUtc { get; set; }

  // Foreign Keys
  public long CourseId { get; init; }

  // Navigation Properties
  public ICollection<File> Files { get; init; } = [];
}