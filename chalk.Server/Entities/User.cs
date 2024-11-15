using Microsoft.AspNetCore.Identity;

namespace chalk.Server.Entities;

public class User : IdentityUser<Guid>
{
    public string? RefreshToken { get; set; }
    public string? RefreshTokenExpiry { get; set; }
}