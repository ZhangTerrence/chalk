using System.Text.Json.Serialization;

namespace chalk.Server.DTOs;

[Serializable]
[method: JsonConstructor]
public record UserDTO(
    [property: JsonRequired]
    [property: JsonPropertyName("id")]
    long UserId,
    [property: JsonRequired]
    [property: JsonPropertyName("displayName")]
    string DisplayName,
    [property: JsonRequired]
    [property: JsonPropertyName("joinedDate")]
    string? JoinedDate);