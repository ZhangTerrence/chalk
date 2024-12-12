using System.Text.Json.Serialization;

namespace chalk.Server.DTOs.Responses;

[Serializable]
[method: JsonConstructor]
public record AuthResponseDTO(
    [property: JsonRequired]
    [property: JsonPropertyName("accessToken")]
    string AccessToken,
    [property: JsonRequired]
    [property: JsonPropertyName("refreshToken")]
    string RefreshToken)
{
}