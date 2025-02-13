namespace Server.Data.Entities;

/// <summary>
/// Represents an organization.
/// </summary>
public class Organization
{
  /// <summary>
  /// The organization's id.
  /// </summary>
  public long Id { get; init; }

  /// <summary>
  /// The organization's name.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The organization's description.
  /// </summary>
  public string? Description { get; set; }

  /// <summary>
  /// The organization's image url.
  /// </summary>
  public string? ImageUrl { get; set; }

  /// <summary>
  /// Whether the organization is public.
  /// </summary>
  public required bool IsPublic { get; set; }

  /// <summary>
  /// The organization's creation date.
  /// </summary>
  public required DateTime CreatedOnUtc { get; init; }

  /// <summary>
  /// The organization's update date.
  /// </summary>
  public required DateTime UpdatedOnUtc { get; set; }

  /// <summary>
  /// The id of the <see cref="User" /> the organization is owned by.
  /// </summary>
  public long OwnerId { get; set; }

  /// <summary>
  /// The organization's users. See <see cref="UserOrganization" /> for more details.
  /// </summary>
  public ICollection<UserOrganization> Users { get; init; } = [];

  /// <summary>
  /// The organization's roles. See <see cref="Role" /> for more details.
  /// </summary>
  public ICollection<Role> Roles { get; init; } = [];

  /// <summary>
  /// The organization's channels. See <see cref="Channel" /> for more details.
  /// </summary>
  public ICollection<Channel> Channels { get; init; } = [];

  /// <summary>
  /// The organization's courses. See <see cref="Course" /> for more details.
  /// </summary>
  public ICollection<Course> Courses { get; init; } = [];

  /// <summary>
  /// The organization's tags. See <see cref="Tag" /> for more details.
  /// </summary>
  public ICollection<Tag> Tags { get; init; } = [];
}
