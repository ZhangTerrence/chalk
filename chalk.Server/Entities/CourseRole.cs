namespace chalk.Server.Entities;

public class CourseRole
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required long Permissions { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public long CourseId { get; set; }

    public Course Course { get; set; } = null!;
    public ICollection<UserCourse> UserCourses { get; set; } = [];
    public ICollection<ChannelParticipant> ChannelParticipants { get; set; } = [];
    public ICollection<ChannelRolePermission> ChannelRolePermissions { get; set; } = [];
}