using System.Text.Json.Serialization;

namespace chalk.Server.DTOs;

[Serializable]
[method: JsonConstructor]
public record ErrorDTO(
    [property: JsonRequired]
    [property: JsonPropertyName("description")]
    string Description
);