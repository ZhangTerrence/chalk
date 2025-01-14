namespace chalk.Server.Entities;

/// <summary>
/// Represents the relationship between a user and a role.
/// </summary>
public class UserRole
{
    // Foreign Keys
    public long UserId { get; set; }
    public long? CourseId { get; set; }
    public long? OrganizationId { get; set; }
    public long RoleId { get; set; }

    // Navigation Properties
    public UserCourse? UserCourse { get; set; }
    public UserOrganization? UserOrganization { get; set; }
    public Role Role { get; set; } = null!;
}