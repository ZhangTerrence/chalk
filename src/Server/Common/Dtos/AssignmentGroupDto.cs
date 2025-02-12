using System.Text.Json.Serialization;

namespace Server.Common.DTOs;

[Serializable]
[method: JsonConstructor]
public sealed record AssignmentGroupDto(
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
  [property: JsonPropertyName("weight")]
  int Weight,
  [property: JsonRequired]
  [property: JsonPropertyName("createdOnUtc")]
  string CreatedOnUtc,
  [property: JsonRequired]
  [property: JsonPropertyName("assignments")]
  IEnumerable<AssignmentDto> Assignments
);