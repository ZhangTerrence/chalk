using System.Text.Json.Serialization;

namespace Server.Common.Responses;

/// <summary>
/// Detailed organization response.
/// </summary>
/// <param name="Id">The organization's id.</param>
/// <param name="Name">The organization's name.</param>
/// <param name="Description">The organization's description.</param>
/// <param name="ImageUrl">The organization's image url.</param>
/// <param name="IsPublic">Whether the organization is public.</param>
/// <param name="CreatedOnUtc">The organization's creation date.</param>
[Serializable]
[method: JsonConstructor]
public record OrganizationResponse(
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
  [property: JsonPropertyName("imageUrl")]
  string? ImageUrl,
  [property: JsonRequired]
  [property: JsonPropertyName("isPublic")]
  bool IsPublic,
  [property: JsonRequired]
  [property: JsonPropertyName("createdOnUtc")]
  string CreatedOnUtc
);
