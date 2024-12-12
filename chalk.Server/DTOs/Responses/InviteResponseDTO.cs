using System.Text.Json.Serialization;
using chalk.Server.Common;

namespace chalk.Server.DTOs.Responses;

[Serializable]
[method: JsonConstructor]
public record InviteResponseDTO(
    [property: JsonRequired]
    [property: JsonPropertyName("inviteType")]
    InviteType InviteType,
    [property: JsonRequired]
    [property: JsonPropertyName("organization")]
    InviteResponseDTO.OrganizationDTO? Organization,
    [property: JsonRequired]
    [property: JsonPropertyName("course")]
    InviteResponseDTO.CourseDTO? Course)
{
    [Serializable]
    [method: JsonConstructor]
    public sealed record OrganizationDTO(
        [property: JsonRequired]
        [property: JsonPropertyName("id")]
        long OrganizationId,
        [property: JsonRequired]
        [property: JsonPropertyName("name")]
        string Name);

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