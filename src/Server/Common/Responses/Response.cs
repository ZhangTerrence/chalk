using System.Text.Json.Serialization;
using Server.Common.DTOs;

namespace Server.Common.Responses;

/// <summary>
/// Base response.
/// </summary>
/// <param name="Errors">The payload errors.</param>
/// <param name="Data">The payload data.</param>
/// <typeparam name="T">The type of the <paramref name="Data" />.</typeparam>
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
  /// <summary>
  /// Constructs a response from a list of errors.
  /// </summary>
  /// <param name="errors">The list of errors.</param>
  public Response(IEnumerable<string> errors) : this(errors.Select(e => new ErrorDto(e)), default)
  {
  }
}
