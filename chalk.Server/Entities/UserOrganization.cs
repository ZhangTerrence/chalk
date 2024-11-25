namespace chalk.Server.Entities;

public class UserOrganization
{
    public required DateTime JoinedDate { get; set; }

    public Guid UserId { get; set; }
    public Guid OrganizationId { get; set; }
    public Guid OrganizationRoleId { get; set; }

    public User User { get; set; } = null!;
    public Organization Organization { get; set; } = null!;
    public OrganizationRole OrganizationRole { get; set; } = null!;
}