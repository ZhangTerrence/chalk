using System.Text.Json.Serialization;
using chalk.Server.Entities;

namespace chalk.Server.DTOs;

[Serializable]
[method: JsonConstructor]
public record CourseDTO(
    [property: JsonRequired]
    [property: JsonPropertyName("id")]
    long Id,
    [property: JsonRequired]
    [property: JsonPropertyName("name")]
    string Name,
    [property: JsonRequired]
    [property: JsonPropertyName("code")]
    string? Code
)
{
    public CourseDTO(Course course) : this(course.Id, course.Name, course.Code)
    {
    }
}