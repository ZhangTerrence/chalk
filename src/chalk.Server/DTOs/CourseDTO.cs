using System.Text.Json.Serialization;

namespace chalk.Server.DTOs;

[Serializable]
[method: JsonConstructor]
public record CourseDTO(
    [property: JsonRequired]
    [property: JsonPropertyName("id")]
    long CourseId,
    [property: JsonRequired]
    [property: JsonPropertyName("name")]
    string Name,
    [property: JsonRequired]
    [property: JsonPropertyName("code")]
    string? Code);