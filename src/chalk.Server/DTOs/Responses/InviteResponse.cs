using System.Text.Json.Serialization;
using chalk.Server.Common;

namespace chalk.Server.DTOs.Responses;

[Serializable]
[method: JsonConstructor]
public record InviteResponse(
    [property: JsonRequired]
    [property: JsonPropertyName("inviteType")]
    InviteType InviteType,
    [property: JsonRequired]
    [property: JsonPropertyName("organization")]
    OrganizationDTO? Organization,
    [property: JsonRequired]
    [property: JsonPropertyName("course")]
    CourseDTO? Course);