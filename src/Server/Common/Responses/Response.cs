using System.Text.Json.Serialization;
using Server.Common.DTOs;

namespace Server.Common.Responses;

[Serializable]
[method: JsonConstructor]
public record Response<T>(
  [property: JsonRequired]
  [property: JsonPropertyName("errors")]
  IEnumerable<ErrorDto>? Errors,
  [property: JsonRequired]
  [property: JsonPropertyName("data")]
  T? Data
)
{
  public Response(IEnumerable<string> errors) : this(errors.Select(e => new ErrorDto(e)), default)
  {
  }
}