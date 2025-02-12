using System.Text.Json.Serialization;

namespace Server.Common.DTOs;

[Serializable]
[method: JsonConstructor]
public sealed record UserDto(
  [property: JsonRequired]
  [property: JsonPropertyName("id")]
  long Id,
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
  [property: JsonPropertyName("joinedOnUtc")]
  string? JoinedOnUtc
);