using Microsoft.AspNetCore.Identity;

namespace chalk.Server.Entities;

public class User : IdentityUser<Guid>
{
    public required string DisplayName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryDate { get; set; }
}