using System.Text.Json.Serialization;

namespace chalk.Server.DTOs;

[Serializable]
[method: JsonConstructor]
public sealed record OrganizationRoleDTO(
    [property: JsonRequired]
    [property: JsonPropertyName("id")]
    long RoleId,
    [property: JsonRequired]
    [property: JsonPropertyName("name")]
    string Name,
    [property: JsonRequired]
    [property: JsonPropertyName("permissions")]
    long Permissions);