using Microsoft.IdentityModel.Tokens;

namespace chalk.Server.DTOs;

public record JwtTokenInfoDTO(
    string Issuer,
    string Audience,
    SymmetricSecurityKey SecurityKey
);

public record JwtClaimInfoDTO(
    long UserId,
    string DisplayName,
    IEnumerable<string> Roles
);