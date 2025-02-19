using System.Text.Json.Serialization;

namespace Server.Common.Responses;

/// <summary>
/// A response error.
/// </summary>
/// <param name="Title">The error's title.</param>
/// <param name="Details">The error's details.</param>
[Serializable]
[method: JsonConstructor]
public sealed record Error(
  [property: JsonRequired]
  [property: JsonPropertyName("title")]
  string Title,
  [property: JsonRequired]
  [property: JsonPropertyName("details")]
  IEnumerable<string> Details
);

/// <summary>
/// Base response.
/// </summary>
/// <param name="Errors">The response's errors.</param>
/// <param name="Data">The response's data.</param>
/// <typeparam name="T">The type of the <paramref name="Data" />.</typeparam>
[Serializable]
[method: JsonConstructor]
public sealed record Response<T>(
  [property: JsonRequired]
  [property: JsonPropertyName("errors")]
  IEnumerable<Error>? Errors,
  [property: JsonRequired]
  [property: JsonPropertyName("data")]
  T? Data
)
{
  /// <summary>
  /// Constructs a response from a dictionary of error titles and details.
  /// </summary>
  public Response(IDictionary<string, string[]> errors)
    : this(errors.Select(pair => new Error(pair.Key, pair.Value)), default)
  {
  }
}
