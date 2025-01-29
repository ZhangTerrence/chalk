using System.Text.Json.Serialization;

namespace chalk.Server.DTOs;

[Serializable]
[method: JsonConstructor]
public record ModuleDTO(
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
    [property: JsonPropertyName("relativeOrder")]
    int RelativeOrder,
    [property: JsonRequired]
    [property: JsonPropertyName("createdDate")]
    string CreatedDate,
    [property: JsonRequired]
    [property: JsonPropertyName("files")]
    IEnumerable<FileDTO> Files
);