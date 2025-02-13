namespace Server.Data.Entities;

/// <summary>
/// Represents an assignment within a course.
/// </summary>
public class Assignment
{
  /// <summary>
  /// The assignment's id.
  /// </summary>
  public long Id { get; init; }

  /// <summary>
  /// The assignment's name.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The assignment's description.
  /// </summary>
  public string? Description { get; set; }

  /// <summary>
  /// The assignment's due date.
  /// </summary>
  public DateTime? DueOnUtc { get; set; }

  /// <summary>
  /// The assignment's creation date.
  /// </summary>
  public required DateTime CreatedOnUtc { get; init; }

  /// <summary>
  /// The assignment's update date.
  /// </summary>
  public required DateTime UpdatedOnUtc { get; set; }

  /// <summary>
  /// The id of the <see cref="AssignmentGroup" /> the assignment belongs in.
  /// </summary>
  public long AssignmentGroupId { get; init; }

  /// <summary>
  /// The assignment's files. See <see cref="File" /> for more details.
  /// </summary>
  public ICollection<File> Files { get; init; } = [];

  /// <summary>
  /// The assignment's submissions See <see cref="Submission" /> for more details.
  /// </summary>
  public ICollection<Submission> Submissions { get; init; } = [];
}
