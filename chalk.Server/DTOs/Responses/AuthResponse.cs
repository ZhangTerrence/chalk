using System.Text.Json.Serialization;

namespace chalk.Server.DTOs.Responses;

[Serializable]
[method: JsonConstructor]
public record AuthResponse(
    [property: JsonRequired]
    [property: JsonPropertyName("user")]
    UserResponse User,
    [property: JsonRequired]
    [property: JsonPropertyName("accessToken")]
    string AccessToken,
    [property: JsonRequired]
    [property: JsonPropertyName("refreshToken")]
    string RefreshToken);