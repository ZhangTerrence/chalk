using System.Text.Json.Serialization;

namespace Server.Common.Responses;

/// <summary>
/// Module response.
/// </summary>
/// <param name="Id">The module's id.</param>
/// <param name="Name">The module's name.</param>
/// <param name="Description">The module's description.</param>
/// <param name="RelativeOrder">The module's relative order compared to other modules in the same course.</param>
/// <param name="CreatedOnUtc">The module's creation date.</param>
/// <param name="UpdatedOnUtc">The module's updated date.</param>
/// <param name="Files">The module's files.</param>
[Serializable]
[method: JsonConstructor]
public sealed record ModuleResponse(
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
  [property: JsonPropertyName("relativeOrder")]
  int RelativeOrder,
  [property: JsonRequired]
  [property: JsonPropertyName("createdOnUtc")]
  string CreatedOnUtc,
  [property: JsonRequired]
  [property: JsonPropertyName("updatedOnUtc")]
  string UpdatedOnUtc,
  [property: JsonRequired]
  [property: JsonPropertyName("files")]
  IEnumerable<FileResponse> Files
) : FileContainerResponse;
