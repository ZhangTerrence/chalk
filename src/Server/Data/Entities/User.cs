using Microsoft.AspNetCore.Identity;

namespace Server.Data.Entities;

/// <summary>
/// Represents a user.
/// </summary>
public class User : IdentityUser<long>
{
  /// <summary>
  /// The user's first name.
  /// </summary>
  public required string FirstName { get; set; }

  /// <summary>
  /// The user's last name.
  /// </summary>
  public required string LastName { get; set; }

  /// <summary>
  /// The user's display name.
  /// </summary>
  public required string DisplayName { get; set; }

  /// <summary>
  /// The user's description.
  /// </summary>
  public string? Description { get; set; }

  /// <summary>
  /// The user's image url.
  /// </summary>
  public string? ImageUrl { get; set; }

  /// <summary>
  /// The user's creation date.
  /// </summary>
  public required DateTime CreatedOnUtc { get; init; }

  /// <summary>
  /// The user's update date.
  /// </summary>
  public required DateTime UpdatedOnUtc { get; set; }

  /// <summary>
  /// The user's direct messages. See <see cref="UserChannel" /> for more details.
  /// </summary>
  public ICollection<UserChannel> DirectMessages { get; init; } = [];

  /// <summary>
  /// The user's courses. See <see cref="UserCourse" /> for more details.
  /// </summary>
  public ICollection<UserCourse> Courses { get; init; } = [];

  /// <summary>
  /// The user's organizations. See <see cref="UserOrganization" /> for more details.
  /// </summary>
  public ICollection<UserOrganization> Organizations { get; init; } = [];
}
