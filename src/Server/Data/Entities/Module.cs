namespace Server.Data.Entities;

/// <summary>
/// Represents a module within a course.
/// </summary>
public class Module
{
  /// <summary>
  /// The module's id.
  /// </summary>
  public long Id { get; init; }

  /// <summary>
  /// The module's name.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The module's description.
  /// </summary>
  public string? Description { get; set; }

  /// <summary>
  /// The module's relative order compared to other modules in the same course.
  /// </summary>
  public required int RelativeOrder { get; set; }

  /// <summary>
  /// The module's creation date.
  /// </summary>
  public required DateTime CreatedOnUtc { get; init; }

  /// <summary>
  /// The module's update date.
  /// </summary>
  public required DateTime UpdatedOnUtc { get; set; }

  /// <summary>
  /// The id of the <see cref="Course" /> the module belongs in.
  /// </summary>
  public long CourseId { get; init; }

  /// <summary>
  /// The module's files. See <see cref="File" /> for more details.
  /// </summary>
  public ICollection<File> Files { get; init; } = [];
}
