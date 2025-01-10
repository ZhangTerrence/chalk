using System.Text.Json.Serialization;

namespace chalk.Server.DTOs.Responses;

[Serializable]
[method: JsonConstructor]
public record ApiResponse<T>(
    [property: JsonRequired]
    [property: JsonPropertyName("errors")]
    IEnumerable<ErrorDTO>? Errors,
    [property: JsonRequired]
    [property: JsonPropertyName("data")]
    T? Data
)
{
    public ApiResponse(IEnumerable<string> errors)
        : this(errors.Select(e => new ErrorDTO(e)), default)
    {
    }
}