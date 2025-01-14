namespace chalk.Server.Entities;

/// <summary>
/// Represents a user-defined role within a course or organization.
/// </summary>
public class Role
{
    // Properties
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required long Permissions { get; set; }
    public required int RelativeRank { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    // Foreign Keys
    public long? CourseId { get; set; }
    public long? OrganizationId { get; set; }
}