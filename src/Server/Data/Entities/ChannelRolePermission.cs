namespace Server.Data.Entities;

/// <summary>
/// Represents the permissions a course or organization role has within a specific channel.
/// </summary>
public class ChannelRolePermission
{
  // Properties
  public long Id { get; init; }
  public required long Permissions { get; set; }

  // Foreign Keys
  public long ChannelId { get; init; }
  public long? CourseRoleId { get; set; }
  public long? OrganizationRoleId { get; set; }
}