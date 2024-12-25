using Microsoft.AspNetCore.Identity;

namespace chalk.Server.Entities;

public class User : IdentityUser<long>
{
    public required string? FirstName { get; set; }
    public required string LastName { get; set; }
    public required string DisplayName { get; set; }
    public string? ProfilePictureUri { get; set; }
    public string? Description { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryDate { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    public ICollection<UserOrganization> UserOrganizations { get; set; } = [];
    public ICollection<UserCourse> UserCourses { get; set; } = [];
    public ICollection<ChannelParticipant> ChannelParticipants { get; set; } = [];
}