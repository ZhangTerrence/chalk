namespace Server.Data.Entities;

/// <summary>
/// Represents a channel within a course or organization. Can also be used for direct messages between users.
/// </summary>
public class Channel
{
  // Properties
  public long Id { get; init; }
  public string? Name { get; set; }
  public string? Description { get; set; }
  public required DateTime CreatedOnUtc { get; init; }
  public required DateTime UpdatedOnUtc { get; set; }

  // Foreign Keys
  public long? CourseId { get; set; }
  public long? OrganizationId { get; set; }

  // Navigation Properties
  public ICollection<UserChannel> Users { get; init; } = [];
  public ICollection<Message> Messages { get; init; } = [];
  public ICollection<ChannelRolePermission> RolePermissions { get; init; } = [];
}