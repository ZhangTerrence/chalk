using System.Text.Json.Serialization;

namespace chalk.Server.DTOs.Responses;

[Serializable]
[method: JsonConstructor]
public record ApiResponse<T>(
    [property: JsonRequired]
    [property: JsonPropertyName("errors")]
    IEnumerable<string>? Errors,
    [property: JsonRequired]
    [property: JsonPropertyName("data")]
    T? Data);