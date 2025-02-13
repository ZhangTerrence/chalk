namespace Server.Data.Entities;

/// <summary>
/// Represents a user submission to an assignment.
/// </summary>
public class Submission
{
  /// <summary>
  /// The submission's id.
  /// </summary>
  public long Id { get; init; }

  /// <summary>
  /// The submission's grade.
  /// </summary>
  public int? Grade { get; set; }

  /// <summary>
  /// The submission's feedback.
  /// </summary>
  public string? Feedback { get; set; }

  /// <summary>
  /// The submission's creation date.
  /// </summary>
  public required DateTime CreatedOnUtc { get; init; }

  /// <summary>
  /// The submission's update date.
  /// </summary>
  public required DateTime UpdatedOnUtc { get; set; }

  /// <summary>
  /// The id of the <see cref="User" /> who created the submission.
  /// </summary>
  public long UserId { get; init; }

  /// <summary>
  /// The id of the <see cref="Assignment" /> the submission was for.
  /// </summary>
  public long AssignmentId { get; init; }

  /// <summary>
  /// The submission's files. See <see cref="File" /> for more details.
  /// </summary>
  public ICollection<File> Files { get; init; } = [];
}
