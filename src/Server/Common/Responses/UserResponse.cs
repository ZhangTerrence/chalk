using System.Text.Json.Serialization;
using Server.Common.DTOs;

namespace Server.Common.Responses;

[Serializable]
[method: JsonConstructor]
public record UserResponse(
  [property: JsonRequired]
  [property: JsonPropertyName("id")]
  long Id,
  [property: JsonRequired]
  [property: JsonPropertyName("email")]
  string Email,
  [property: JsonRequired]
  [property: JsonPropertyName("firstName")]
  string FirstName,
  [property: JsonRequired]
  [property: JsonPropertyName("lastName")]
  string LastName,
  [property: JsonRequired]
  [property: JsonPropertyName("displayName")]
  string DisplayName,
  [property: JsonRequired]
  [property: JsonPropertyName("description")]
  string? Description,
  [property: JsonRequired]
  [property: JsonPropertyName("imageUrl")]
  string? ImageUrl,
  [property: JsonRequired]
  [property: JsonPropertyName("createdOnUtc")]
  string CreatedOnUtc,
  [property: JsonRequired]
  [property: JsonPropertyName("directMessages")]
  IEnumerable<ChannelDto> DirectMessages,
  [property: JsonRequired]
  [property: JsonPropertyName("courses")]
  IEnumerable<CourseDto> Courses,
  [property: JsonRequired]
  [property: JsonPropertyName("organizations")]
  IEnumerable<OrganizationDto> Organizations
);