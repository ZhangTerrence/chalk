using System.Text.Json.Serialization;

namespace chalk.Server.DTOs;

[Serializable]
[method: JsonConstructor]
public record UserDTO(
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
    [property: JsonPropertyName("createdDate")]
    string CreatedDate,
    [property: JsonRequired]
    [property: JsonPropertyName("joinedDate")]
    string? JoinedDate
);