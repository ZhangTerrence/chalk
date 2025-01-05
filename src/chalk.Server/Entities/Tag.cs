namespace chalk.Server.Entities;

/// <summary>
/// Represents a user-given tag for a course or organization.
/// </summary>
public class Tag
{
    // Properties
    public long Id { get; set; }
    public required string Name { get; set; }

    // Foreign Keys
    public long? CourseId { get; set; }
    public long? OrganizationId { get; set; }
}