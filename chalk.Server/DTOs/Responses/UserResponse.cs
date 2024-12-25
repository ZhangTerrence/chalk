using System.Text.Json.Serialization;

namespace chalk.Server.DTOs.Responses;

[Serializable]
[method: JsonConstructor]
public record UserResponse(
    [property: JsonRequired]
    [property: JsonPropertyName("id")]
    long Id,
    [property: JsonRequired]
    [property: JsonPropertyName("email")]
    string? Email,
    [property: JsonRequired]
    [property: JsonPropertyName("firstName")]
    string? FirstName,
    [property: JsonRequired]
    [property: JsonPropertyName("lastName")]
    string LastName,
    [property: JsonRequired]
    [property: JsonPropertyName("displayName")]
    string DisplayName,
    [property: JsonRequired]
    [property: JsonPropertyName("profilePicture")]
    string? ProfilePicture,
    [property: JsonRequired]
    [property: JsonPropertyName("createdDate")]
    string CreatedDate,
    [property: JsonRequired]
    [property: JsonPropertyName("updatedDate")]
    string UpdatedDate,
    [property: JsonRequired]
    [property: JsonPropertyName("organizations")]
    IEnumerable<OrganizationDTO> Organizations,
    [property: JsonRequired]
    [property: JsonPropertyName("courses")]
    IEnumerable<CourseDTO> Courses,
    [property: JsonRequired]
    [property: JsonPropertyName("channels")]
    IEnumerable<ChannelDTO> Channels);