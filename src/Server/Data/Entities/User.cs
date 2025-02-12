using Microsoft.AspNetCore.Identity;

namespace Server.Data.Entities;

/// <summary>
/// Represents a user.
/// </summary>
public class User : IdentityUser<long>
{
  // Properties
  public required string FirstName { get; set; }
  public required string LastName { get; set; }
  public required string DisplayName { get; set; }
  public string? Description { get; set; }
  public string? ImageUrl { get; set; }
  public required DateTime CreatedOnUtc { get; init; }
  public required DateTime UpdatedOnUtc { get; set; }

  // Navigation Properties
  public ICollection<UserChannel> DirectMessages { get; init; } = [];
  public ICollection<UserCourse> Courses { get; init; } = [];
  public ICollection<UserOrganization> Organizations { get; init; } = [];
}