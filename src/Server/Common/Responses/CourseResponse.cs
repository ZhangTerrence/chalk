using System.Text.Json.Serialization;
using Server.Common.DTOs;

namespace Server.Common.Responses;

/// <summary>
/// Detailed course response.
/// </summary>
/// <param name="Id">The course's id.</param>
/// <param name="Name">The course's name.</param>
/// <param name="Code">The course's code.</param>
/// <param name="Description">The course's description.</param>
/// <param name="ImageUrl">The course's image url.</param>
/// <param name="IsPublic">Whether the course is public.</param>
/// <param name="CreatedOnUtc">The course's creation date.</param>
/// <param name="Modules">The course's modules.</param>
/// <param name="AssignmentGroups">The course's assignment groups.</param>
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
