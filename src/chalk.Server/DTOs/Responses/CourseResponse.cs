using System.Text.Json.Serialization;

namespace chalk.Server.DTOs.Responses;

[Serializable]
[method: JsonConstructor]
public record CourseResponse(
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
    [property: JsonPropertyName("imageUrl")]
    string? ImageUrl,
    [property: JsonRequired]
    [property: JsonPropertyName("isPublic")]
    bool IsPublic,
    [property: JsonRequired]
    [property: JsonPropertyName("createdDate")]
    string CreatedDate,
    [property: JsonRequired]
    [property: JsonPropertyName("modules")]
    IEnumerable<ModuleDTO> Modules,
    [property: JsonRequired]
    [property: JsonPropertyName("assignmentGroups")]
    IEnumerable<AssignmentGroupDTO> AssignmentGroups
);