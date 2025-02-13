namespace Server.Data.Entities;

/// <summary>
/// Represents an assignment group within a course.
/// </summary>
public class AssignmentGroup
{
  // Properties
  /// <summary>
  /// The assignment group's id.
  /// </summary>
  public long Id { get; init; }

  /// <summary>
  /// The assignment group's name.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The assignment group's description.
  /// </summary>
  public string? Description { get; set; }

  /// <summary>
  /// The assignment group's weight.
  /// </summary>
  public required int Weight { get; set; }

  /// <summary>
  /// The assignment group's creation date.
  /// </summary>
  public required DateTime CreatedOnUtc { get; init; }

  /// <summary>
  /// The assignment group's update date.
  /// </summary>
  public required DateTime UpdatedOnUtc { get; set; }

  /// <summary>
  /// The id of the <see cref="Course" /> the assignment group belongs in.
  /// </summary>
  public long CourseId { get; init; }

  /// <summary>
  /// The assignment group's assignments. See <see cref="Assignment" /> for more details.
  /// </summary>
  public ICollection<Assignment> Assignments { get; init; } = [];
}
