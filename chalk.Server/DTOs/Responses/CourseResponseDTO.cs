using System.Text.Json.Serialization;

namespace chalk.Server.DTOs.Responses;

[Serializable]
[method: JsonConstructor]
public record CourseResponseDTO(
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
    [property: JsonPropertyName("createdDate")]
    string CreatedDate,
    [property: JsonRequired]
    [property: JsonPropertyName("updatedDate")]
    string UpdatedDate,
    [property: JsonRequired]
    [property: JsonPropertyName("organization")]
    CourseResponseDTO.OrganizationDTO Organization,
    [property: JsonRequired]
    [property: JsonPropertyName("users")]
    IEnumerable<CourseResponseDTO.UserDTO> Users,
    [property: JsonRequired]
    [property: JsonPropertyName("users")]
    IEnumerable<CourseResponseDTO.CourseRoleDTO> Roles
)
{
    [Serializable]
    [method: JsonConstructor]
    public record OrganizationDTO(
        [property: JsonRequired]
        [property: JsonPropertyName("id")]
        long OrganizationId,
        [property: JsonRequired]
        [property: JsonPropertyName("name")]
        string Name);

    [Serializable]
    [method: JsonConstructor]
    public sealed record UserDTO(
        [property: JsonRequired]
        [property: JsonPropertyName("id")]
        long UserId,
        [property: JsonRequired]
        [property: JsonPropertyName("displayName")]
        string DisplayName,
        [property: JsonRequired]
        [property: JsonPropertyName("joinedDate")]
        string? JoinedDate);

    [Serializable]
    [method: JsonConstructor]
    public sealed record CourseRoleDTO(
        [property: JsonRequired]
        [property: JsonPropertyName("id")]
        long RoleId,
        [property: JsonRequired]
        [property: JsonPropertyName("name")]
        string Name,
        [property: JsonRequired]
        [property: JsonPropertyName("permissions")]
        long Permissions);
}