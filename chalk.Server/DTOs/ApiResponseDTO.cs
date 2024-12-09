using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace chalk.Server.DTOs;

[Serializable]
[method: JsonConstructor]
public record ApiResponseDTO<T>(
    [property: JsonRequired]
    [property: JsonPropertyName("errors")]
    IEnumerable<string>? Errors,
    [property: JsonRequired]
    [property: JsonPropertyName("data")]
    T? Data)
{
    public ApiResponseDTO(ModelStateDictionary modelState) :
        this(modelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)), default)
    {
    }
}