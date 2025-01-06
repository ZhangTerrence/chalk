using System.Text.Json.Serialization;
using chalk.Server.Entities;

namespace chalk.Server.DTOs;

[Serializable]
[method: JsonConstructor]
public sealed record OrganizationRoleDTO(
    [property: JsonRequired]
    [property: JsonPropertyName("id")]
    long Id,
    [property: JsonRequired]
    [property: JsonPropertyName("name")]
    string Name,
    [property: JsonRequired]
    [property: JsonPropertyName("permissions")]
    long Permissions
)
{
    public OrganizationRoleDTO(OrganizationRole organizationRole)
        : this(organizationRole.Id, organizationRole.Name, organizationRole.Permissions)
    {
    }
}