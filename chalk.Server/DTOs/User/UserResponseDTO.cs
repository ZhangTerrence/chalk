using System.Text.Json.Serialization;

namespace chalk.Server.DTOs.User;

[Serializable]
[method: JsonConstructor]
public record UserResponseDTO(
    [property: JsonRequired]
    [property: JsonPropertyName("id")]
    long Id,
    [property: JsonRequired]
    [property: JsonPropertyName("email")]
    string? Email,
    [property: JsonRequired]
    [property: JsonPropertyName("fullName")]
    string FullName,
    [property: JsonRequired]
    [property: JsonPropertyName("displayName")]
    string DisplayName,
    [property: JsonRequired]
    [property: JsonPropertyName("profilePicture")]
    string? ProfilePicture,
    [property: JsonRequired]
    [property: JsonPropertyName("createdDate")]
    DateTime CreatedDate,
    [property: JsonRequired]
    [property: JsonPropertyName("updatedDate")]
    DateTime UpdatedDate,
    [property: JsonRequired]
    [property: JsonPropertyName("organizations")]
    IEnumerable<UserResponseDTO.OrganizationDTO> Organizations,
    [property: JsonRequired]
    [property: JsonPropertyName("courses")]
    IEnumerable<UserResponseDTO.CourseDTO> Courses,
    [property: JsonRequired]
    [property: JsonPropertyName("channels")]
    IEnumerable<UserResponseDTO.ChannelDTO> Channels)
{
    [Serializable]
    [method: JsonConstructor]
    public record OrganizationDTO(
        [property: JsonRequired]
        [property: JsonPropertyName("organizationId")]
        long OrganizationId,
        [property: JsonRequired]
        [property: JsonPropertyName("name")]
        string Name);

    [Serializable]
    [method: JsonConstructor]
    public record CourseDTO(
        [property: JsonRequired]
        [property: JsonPropertyName("courseId")]
        long CourseId,
        [property: JsonRequired]
        [property: JsonPropertyName("name")]
        string Name,
        [property: JsonRequired]
        [property: JsonPropertyName("code")]
        string Code);

    [Serializable]
    [method: JsonConstructor]
    public record ChannelDTO(
        [property: JsonRequired]
        [property: JsonPropertyName("channelId")]
        long ChannelId,
        [property: JsonRequired]
        [property: JsonPropertyName("name")]
        string Name);
}