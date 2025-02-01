using System.Text.Json.Serialization;

namespace chalk.Server.DTOs.Responses;

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
    [property: JsonPropertyName("createdDate")]
    string CreatedDate
);