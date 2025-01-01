using chalk.Server.Shared;

namespace chalk.Server.Entities;

public class UserOrganization
{
    public required Status Status { get; set; }
    public DateTime? JoinedDate { get; set; }

    public long UserId { get; set; }
    public long OrganizationId { get; set; }
    public long OrganizationRoleId { get; set; }

    public User User { get; set; } = null!;
    public Organization Organization { get; set; } = null!;
    public OrganizationRole OrganizationRole { get; set; } = null!;
}