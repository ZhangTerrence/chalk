using System.Text.Json.Serialization;

namespace chalk.Server.DTOs;

[Serializable]
[method: JsonConstructor]
public record OrganizationDTO(
    [property: JsonRequired]
    [property: JsonPropertyName("id")]
    long OrganizationId,
    [property: JsonRequired]
    [property: JsonPropertyName("name")]
    string Name);