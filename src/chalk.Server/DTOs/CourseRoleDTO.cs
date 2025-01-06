using System.Text.Json.Serialization;
using chalk.Server.Entities;

namespace chalk.Server.DTOs;

[Serializable]
[method: JsonConstructor]
public sealed record CourseRoleDTO(
    [property: JsonRequired]
    [property: JsonPropertyName("id")]
    long Id,
    [property: JsonRequired]
    [property: JsonPropertyName("name")]
    string Name,
    [property: JsonRequired]
    [property: JsonPropertyName("permissions")]
    long Permissions
)
{
    public CourseRoleDTO(CourseRole courseRole) : this(courseRole.Id, courseRole.Name, courseRole.Permissions)
    {
    }
}