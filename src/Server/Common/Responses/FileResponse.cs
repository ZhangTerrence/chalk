using System.Text.Json.Serialization;

namespace Server.Common.Responses;

/// <summary>
/// File container response.
/// </summary>
public abstract record FileContainerResponse;

/// <summary>
/// File response.
/// </summary>
/// <param name="Id">The file's id.</param>
/// <param name="Name">The file's name.</param>
/// <param name="Description">The file's description.</param>
/// <param name="FileUrl">The file's storage url.</param>
/// <param name="CreatedOnUtc">The file's creation date.</param>
/// <param name="UpdatedOnUtc">The file's updated date.</param>
[Serializable]
[method: JsonConstructor]
public sealed record FileResponse(
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
  [property: JsonPropertyName("fileUrl")]
  string FileUrl,
  [property: JsonRequired]
  [property: JsonPropertyName("createdOnUtc")]
  string CreatedOnUtc,
  [property: JsonRequired]
  [property: JsonPropertyName("updatedOnUtc")]
  string UpdatedOnUtc
);
