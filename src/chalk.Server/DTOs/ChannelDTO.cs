using System.Text.Json.Serialization;
using chalk.Server.Entities;

namespace chalk.Server.DTOs;

[Serializable]
[method: JsonConstructor]
public record ChannelDTO(
    [property: JsonRequired]
    [property: JsonPropertyName("id")]
    long Id,
    [property: JsonRequired]
    [property: JsonPropertyName("name")]
    string? Name
)
{
    public ChannelDTO(Channel channel) : this(channel.Id, channel.Name)
    {
    }
}