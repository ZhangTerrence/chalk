using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace chalk.Server.DTOs;

public record ApiResponseDTO<T>
{
    public ApiResponseDTO(string error)
    {
        Errors = [error];
    }

    public ApiResponseDTO(IEnumerable<string> errors)
    {
        Errors = errors;
    }

    public ApiResponseDTO(ModelStateDictionary modelState)
    {
        Errors = modelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
    }

    public ApiResponseDTO(T data)
    {
        Data = data;
    }

    public IEnumerable<string>? Errors { get; init; }
    public T? Data { get; init; }
}