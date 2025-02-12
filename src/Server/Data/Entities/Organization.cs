namespace Server.Data.Entities;

/// <summary>
/// Represents an organization.
/// </summary>
public class Organization
{
  // Properties
  public long Id { get; init; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public string? ImageUrl { get; set; }
  public required bool IsPublic { get; set; }
  public required DateTime CreatedOnUtc { get; init; }
  public required DateTime UpdatedOnUtc { get; set; }

  // Foreign Keys
  public long OwnerId { get; init; }

  // Navigation Properties
  public User Owner { get; init; } = null!;
  public ICollection<UserOrganization> Users { get; init; } = [];
  public ICollection<Role> Roles { get; init; } = [];
  public ICollection<Channel> Channels { get; init; } = [];
  public ICollection<Course> Courses { get; init; } = [];
  public ICollection<Tag> Tags { get; init; } = [];
}