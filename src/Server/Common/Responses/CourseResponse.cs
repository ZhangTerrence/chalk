using System.Text.Json.Serialization;
using Server.Common.DTOs;

namespace Server.Common.Responses;

[Serializable]
[method: JsonConstructor]
public record CourseResponse(
  [property: JsonRequired]
  [property: JsonPropertyName("id")]
  long Id,
  [property: JsonRequired]
  [property: JsonPropertyName("name")]
  string Name,
  [property: JsonRequired]
  [property: JsonPropertyName("code")]
  string? Code,
  [property: JsonRequired]
  [property: JsonPropertyName("description")]
  string? Description,
  [property: JsonRequired]
  [property: JsonPropertyName("imageUrl")]
  string? ImageUrl,
  [property: JsonRequired]
  [property: JsonPropertyName("isPublic")]
  bool IsPublic,
  [property: JsonRequired]
  [property: JsonPropertyName("createdOnUtc")]
  string CreatedOnUtc,
  [property: JsonRequired]
  [property: JsonPropertyName("modules")]
  IEnumerable<ModuleDto> Modules,
  [property: JsonRequired]
  [property: JsonPropertyName("assignmentGroups")]
  IEnumerable<AssignmentGroupDto> AssignmentGroups
);