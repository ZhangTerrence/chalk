using System.Security.Claims;
using Server.Common.Exceptions;
using Server.Data.Entities;

namespace Server.Common.Utilities;

public static class AuthUtilities
{
  public static long GetUserId(this ClaimsPrincipal principal)
  {
    var id = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (id is null) ServiceException.Unauthorized("Login required.", id);
    return long.Parse(id);
  }

  public static IEnumerable<Claim> CreateClaims(this User user, IEnumerable<string> roles)
  {
    List<Claim> claims =
    [
      new(ClaimTypes.NameIdentifier, user.Id.ToString()),
      new(ClaimTypes.Name, user.DisplayName)
    ];
    claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
    return claims;
  }
}