using System.Text.Json.Serialization;
using chalk.Server.Shared;

namespace chalk.Server.DTOs.Responses;

[Serializable]
[method: JsonConstructor]
public record InviteResponse(
    [property: JsonRequired]
    [property: JsonPropertyName("inviteType")]
    Invite Invite,
    [property: JsonRequired]
    [property: JsonPropertyName("organization")]
    OrganizationDTO? Organization,
    [property: JsonRequired]
    [property: JsonPropertyName("course")]
    CourseDTO? Course);