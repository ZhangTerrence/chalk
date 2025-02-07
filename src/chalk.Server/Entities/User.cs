using Microsoft.AspNetCore.Identity;

namespace chalk.Server.Entities;

/// <summary>
/// Represents a user.
/// </summary>
public class User : IdentityUser<long>
{
    // Properties
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string DisplayName { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    // Navigation Properties
    public ICollection<UserChannel> DirectMessages { get; set; } = [];
    public ICollection<UserCourse> Courses { get; set; } = [];
    public ICollection<UserOrganization> Organizations { get; set; } = [];
}