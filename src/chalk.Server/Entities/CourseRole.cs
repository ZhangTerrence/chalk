namespace chalk.Server.Entities;

/// <summary>
/// Represents a user-defined role within a course.
/// </summary>
public class CourseRole
{
    // Properties
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required long Permissions { get; set; }
    public required int Rank { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    // Foreign Keys
    public long CourseId { get; set; }

    // Navigation Properties
    public Course Course { get; set; } = null!;
    public ICollection<UserCourse> Users { get; set; } = [];
    public ICollection<UserChannel> Channels { get; set; } = [];
    public ICollection<ChannelRolePermission> ChannelPermissions { get; set; } = [];
}