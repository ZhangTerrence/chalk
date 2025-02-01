using System.Text.Json.Serialization;

namespace chalk.Server.DTOs;

[Serializable]
[method: JsonConstructor]
public record AssignmentDTO(
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
    [property: JsonPropertyName("isOpen")]
    bool IsOpen,
    [property: JsonRequired]
    [property: JsonPropertyName("dueDate")]
    string? DueDate,
    [property: JsonRequired]
    [property: JsonPropertyName("allowedAttempts")]
    int? AllowedAttempts,
    [property: JsonRequired]
    [property: JsonPropertyName("createdDate")]
    string CreatedDate,
    [property: JsonRequired]
    [property: JsonPropertyName("updatedDate")]
    string UpdatedDate
);