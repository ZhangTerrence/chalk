namespace chalk.Server.Entities;

/// <summary>
/// Represents a user-defined role within an organization.
/// </summary>
public class OrganizationRole
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
    public long OrganizationId { get; set; }

    // Navigation Properties
    public Organization Organization { get; set; } = null!;
    public ICollection<UserOrganization> Users { get; set; } = [];
    public ICollection<UserChannel> Channels { get; set; } = [];
    public ICollection<ChannelRolePermission> ChannelPermissions { get; set; } = [];
}