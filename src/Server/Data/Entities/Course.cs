namespace Server.Data.Entities;

/// <summary>
/// Represents a course.
/// </summary>
public class Course
{
  /// <summary>
  /// The course's id.
  /// </summary>
  public long Id { get; init; }

  /// <summary>
  /// The course's name.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The course's code.
  /// </summary>
  public string? Code { get; set; }

  /// <summary>
  /// The course's description.
  /// </summary>
  public string? Description { get; set; }

  /// <summary>
  /// The course's image url.
  /// </summary>
  public string? ImageUrl { get; set; }

  /// <summary>
  /// Whether the course is public.
  /// </summary>
  public required bool IsPublic { get; set; }

  /// <summary>
  /// The course's creation date.
  /// </summary>
  public required DateTime CreatedOnUtc { get; init; }

  /// <summary>
  /// The course's update date.
  /// </summary>
  public required DateTime UpdatedOnUtc { get; set; }

  /// <summary>
  /// The id of the <see cref="Organization" /> the course could belong in.
  /// </summary>
  public long? OrganizationId { get; set; }

  /// <summary>
  /// The course's users. See <see cref="UserCourse" /> for more details.
  /// </summary>
  public ICollection<UserCourse> Users { get; init; } = [];

  /// <summary>
  /// The course's roles. See <see cref="Role" /> for more details.
  /// </summary>
  public ICollection<Role> Roles { get; init; } = [];

  /// <summary>
  /// The course's modules. See <see cref="Module" /> for more details.
  /// </summary>
  public ICollection<Module> Modules { get; init; } = [];

  /// <summary>
  /// The course's assignment groups. See <see cref="AssignmentGroup" /> for more details.
  /// </summary>
  public ICollection<AssignmentGroup> AssignmentGroups { get; init; } = [];

  /// <summary>
  /// The course's channels. See <see cref="Channel" /> for more details.
  /// </summary>
  public ICollection<Channel> Channels { get; init; } = [];

  /// <summary>
  /// The course's tags. See <see cref="Tag" /> for more details.
  /// </summary>
  public ICollection<Tag> Tags { get; init; } = [];
}
