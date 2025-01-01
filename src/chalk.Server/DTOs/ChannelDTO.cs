using System.Text.Json.Serialization;

namespace chalk.Server.DTOs;

[Serializable]
[method: JsonConstructor]
public record ChannelDTO(
    [property: JsonRequired]
    [property: JsonPropertyName("id")]
    long ChannelId,
    [property: JsonRequired]
    [property: JsonPropertyName("name")]
    string? Name);