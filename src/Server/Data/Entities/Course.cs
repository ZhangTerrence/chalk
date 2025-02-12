namespace Server.Data.Entities;

/// <summary>
/// Represents a course.
/// </summary>
public class Course
{
  // Properties
  public long Id { get; init; }
  public required string Name { get; set; }
  public string? Code { get; set; }
  public string? Description { get; set; }
  public string? ImageUrl { get; set; }
  public required bool IsPublic { get; set; }
  public required DateTime CreatedOnUtc { get; init; }
  public required DateTime UpdatedOnUtc { get; set; }

  // Foreign Keys
  public long? OrganizationId { get; set; }

  // Navigation Properties
  public Organization? Organization { get; set; }
  public ICollection<UserCourse> Users { get; init; } = [];
  public ICollection<Role> Roles { get; init; } = [];
  public ICollection<Module> Modules { get; init; } = [];
  public ICollection<AssignmentGroup> AssignmentGroups { get; init; } = [];
  public ICollection<Channel> Channels { get; init; } = [];
  public ICollection<Tag> Tags { get; init; } = [];
}