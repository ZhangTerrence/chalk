using System.Text.Json.Serialization;

namespace chalk.Server.DTOs.Responses;

[Serializable]
[method: JsonConstructor]
public record CourseResponse(
    [property: JsonRequired]
    [property: JsonPropertyName("id")]
    long Id,
    [property: JsonRequired]
    [property: JsonPropertyName("name")]
    string Name,
    [property: JsonRequired]
    [property: JsonPropertyName("code")]
    string? Code,
    [property: JsonRequired]
    [property: JsonPropertyName("description")]
    string? Description,
    [property: JsonRequired]
    [property: JsonPropertyName("image")]
    string? Image,
    [property: JsonRequired]
    [property: JsonPropertyName("isPublic")]
    bool IsPublic,
    [property: JsonRequired]
    [property: JsonPropertyName("createdDate")]
    string CreatedDate,
    [property: JsonRequired]
    [property: JsonPropertyName("organization")]
    OrganizationDTO? Organization,
    [property: JsonRequired]
    [property: JsonPropertyName("users")]
    IEnumerable<UserDTO> Users,
    [property: JsonRequired]
    [property: JsonPropertyName("roles")]
    IEnumerable<RoleDTO> Roles
);