using chalk.Server.Shared;

namespace chalk.Server.Entities;

/// <summary>
/// Represents the relationship between a user and an organization.
/// </summary>
public class UserOrganization
{
    // Properties
    public required UserStatus Status { get; set; }
    public DateTime? JoinedDate { get; set; }

    // Foreign Keys
    public long UserId { get; set; }
    public long OrganizationId { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public Organization Organization { get; set; } = null!;
    public ICollection<UserRole> Roles { get; set; } = [];
}