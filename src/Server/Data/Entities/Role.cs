namespace Server.Data.Entities;

/// <summary>
/// Represents a user-defined role within a course or organization.
/// </summary>
public class Role
{
  /// <summary>
  /// The role's id.
  /// </summary>
  public long Id { get; init; }

  /// <summary>
  /// The role's name.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The role's description.
  /// </summary>
  public string? Description { get; set; }

  /// <summary>
  /// The role's permissions.
  /// </summary>
  public required long Permissions { get; set; }

  /// <summary>
  /// The role's relative rank compared to other ranks in the same course or organization.
  /// </summary>
  public required int RelativeRank { get; set; }

  /// <summary>
  /// The role's creation date.
  /// </summary>
  public required DateTime CreatedOnUtc { get; init; }

  /// <summary>
  /// The role's update date.
  /// </summary>
  public required DateTime UpdatedOnUtc { get; set; }

  /// <summary>
  /// The id of the <see cref="Course" /> the role could belong in.
  /// </summary>
  public long? CourseId { get; init; }

  /// <summary>
  /// The id of the <see cref="Organization" /> the role could belong in.
  /// </summary>
  public long? OrganizationId { get; init; }
}
