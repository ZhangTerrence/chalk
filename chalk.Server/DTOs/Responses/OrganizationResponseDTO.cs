using System.Text.Json.Serialization;

namespace chalk.Server.DTOs.Responses;

[Serializable]
[method: JsonConstructor]
public record OrganizationResponseDTO(
    [property: JsonRequired]
    [property: JsonPropertyName("id")]
    long Id,
    [property: JsonRequired]
    [property: JsonPropertyName("name")]
    string Name,
    [property: JsonRequired]
    [property: JsonPropertyName("profilePictureUri")]
    string? ProfilePictureUri,
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
    [property: JsonPropertyName("owner")]
    OrganizationResponseDTO.UserDTO Owner,
    [property: JsonRequired]
    [property: JsonPropertyName("users")]
    IEnumerable<OrganizationResponseDTO.UserDTO> Users,
    [property: JsonRequired]
    [property: JsonPropertyName("roles")]
    IEnumerable<OrganizationResponseDTO.OrganizationRoleDTO> Roles,
    [property: JsonRequired]
    [property: JsonPropertyName("courses")]
    IEnumerable<OrganizationResponseDTO.CourseDTO> Courses)
{
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

    [Serializable]
    [method: JsonConstructor]
    public sealed record CourseDTO(
        [property: JsonRequired]
        [property: JsonPropertyName("id")]
        long CourseId,
        [property: JsonRequired]
        [property: JsonPropertyName("name")]
        string Name,
        [property: JsonRequired]
        [property: JsonPropertyName("code")]
        string? Code);
}