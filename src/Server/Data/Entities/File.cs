namespace Server.Data.Entities;

/// <summary>
/// Represents a file attached to either a module, assignment, or submission.
/// </summary>
public class File
{
  /// <summary>
  /// The file's id.
  /// </summary>
  public long Id { get; init; }

  /// <summary>
  /// The file's name.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The file's description.
  /// </summary>
  public string? Description { get; set; }

  /// <summary>
  /// The file's storage url.
  /// </summary>
  public required string FileUrl { get; set; }

  /// <summary>
  /// The file's creation date.
  /// </summary>
  public required DateTime CreatedOnUtc { get; init; }

  /// <summary>
  /// The file's update date.
  /// </summary>
  public required DateTime UpdatedOnUtc { get; set; }

  /// <summary>
  /// The id of the <see cref="Assignment" /> the file could be attached to.
  /// </summary>
  public long? AssignmentId { get; init; }

  /// <summary>
  /// The id of the <see cref="Submission" /> the file could be attached to.
  /// </summary>
  public long? SubmissionId { get; init; }

  /// <summary>
  /// The id of the <see cref="Module" /> the file could be attached to.
  /// </summary>
  public long? ModuleId { get; init; }
}
