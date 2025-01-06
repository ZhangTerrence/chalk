using System.Text.Json.Serialization;
using chalk.Server.Entities;

namespace chalk.Server.DTOs;

[Serializable]
[method: JsonConstructor]
public record OrganizationDTO(
    [property: JsonRequired]
    [property: JsonPropertyName("id")]
    long Id,
    [property: JsonRequired]
    [property: JsonPropertyName("name")]
    string Name
)
{
    public OrganizationDTO(Organization organization) : this(organization.Id, organization.Name)
    {
    }
}