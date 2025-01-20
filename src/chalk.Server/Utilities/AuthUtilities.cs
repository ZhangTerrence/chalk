using System.Security.Claims;
using chalk.Server.Configurations;
using chalk.Server.Entities;

namespace chalk.Server.Utilities;

public static class AuthUtilities
{
    public static long GetUserId(this ClaimsPrincipal principal)
    {
        var id = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (id is null)
        {
            throw new ServiceException("Not authenticated.", StatusCodes.Status401Unauthorized);
        }

        return long.Parse(id);
    }

    public static IEnumerable<Claim> CreateClaims(this User user, IEnumerable<string> roles)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.DisplayName)
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }
}