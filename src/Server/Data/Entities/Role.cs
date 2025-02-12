namespace Server.Data.Entities;

/// <summary>
/// Represents a user-defined role within a course or organization.
/// </summary>
public class Role
{
  // Properties
  public long Id { get; init; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public required long Permissions { get; set; }
  public required int RelativeRank { get; set; }
  public required DateTime CreatedOnUtc { get; init; }
  public required DateTime UpdatedOnUtc { get; set; }

  // Foreign Keys
  public long? CourseId { get; set; }
  public long? OrganizationId { get; set; }
}