using System.Text.Json.Serialization;

namespace chalk.Server.DTOs.Responses;

[Serializable]
[method: JsonConstructor]
public record OrganizationResponse(
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
    [property: JsonPropertyName("profilePicture")]
    string? ProfilePicture,
    [property: JsonRequired]
    [property: JsonPropertyName("createdDate")]
    string CreatedDate,
    [property: JsonRequired]
    [property: JsonPropertyName("owner")]
    UserDTO Owner,
    [property: JsonRequired]
    [property: JsonPropertyName("users")]
    IEnumerable<UserDTO> Users,
    [property: JsonRequired]
    [property: JsonPropertyName("roles")]
    IEnumerable<RoleDTO> Roles,
    [property: JsonRequired]
    [property: JsonPropertyName("channels")]
    IEnumerable<ChannelDTO> Channels,
    [property: JsonRequired]
    [property: JsonPropertyName("courses")]
    IEnumerable<CourseDTO> Courses
);