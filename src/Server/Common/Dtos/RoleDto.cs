using System.Text.Json.Serialization;

namespace Server.Common.DTOs;

[Serializable]
[method: JsonConstructor]
public sealed record RoleDto(
  [property: JsonRequired]
  [property: JsonPropertyName("id")]
  long Id,
  [property: JsonRequired]
  [property: JsonPropertyName("name")]
  string Name,
  [property: JsonRequired]
  [property: JsonPropertyName("description")]
  string? Description,
  [property: JsonRequired]
  [property: JsonPropertyName("permissions")]
  long Permissions,
  [property: JsonRequired]
  [property: JsonPropertyName("relativeRank")]
  int RelativeRank
);