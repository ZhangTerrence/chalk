namespace Server.Data.Entities;

/// <summary>
/// Represents a user-given tag for a course or organization.
/// </summary>
public class Tag
{
  /// <summary>
  /// The tag's id.
  /// </summary>
  public long Id { get; init; }

  /// <summary>
  /// The tag's name.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The id of the <see cref="Course" /> the tag could be for.
  /// </summary>
  public long? CourseId { get; init; }

  /// <summary>
  /// The id of the <see cref="Organization" /> the tag could be for.
  /// </summary>
  public long? OrganizationId { get; init; }
}
