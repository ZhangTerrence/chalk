namespace Server.Data.Entities;

/// <summary>
/// Represents a user-given tag for a course or organization.
/// </summary>
public class Tag
{
  // Properties
  public long Id { get; init; }
  public required string Name { get; set; }

  // Foreign Keys
  public long? CourseId { get; set; }
  public long? OrganizationId { get; set; }
}