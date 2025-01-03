namespace chalk.Server.Entities;

public class CourseRole
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required long Permissions { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    public long CourseId { get; set; }

    public Course Course { get; set; } = null!;
    public ICollection<UserCourse> Users { get; set; } = [];
    public ICollection<ChannelUser> Channels { get; set; } = [];
    public ICollection<ChannelRolePermission> ChannelPermissions { get; set; } = [];
}