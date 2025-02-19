namespace Server.Common.DTOs;

internal sealed record RoleDto(
  long Id,
  string Name,
  string? Description,
  long Permissions,
  int RelativeRank
);
