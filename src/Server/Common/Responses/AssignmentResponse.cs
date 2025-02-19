using System.Text.Json.Serialization;

namespace Server.Common.Responses;

/// <summary>
/// Assignment response.
/// </summary>
/// <param name="Id">The assignment's id.</param>
/// <param name="Name">The assignment's name.</param>
/// <param name="Description">The assignment's description.</param>
/// <param name="DueOnUtc">The assignment's due date.</param>
/// <param name="CreatedOnUtc">The assignment's creation date.</param>
/// <param name="UpdatedOnUtc">The assignment's updated date.</param>
/// <param name="Files">The assignment's files.</param>
[Serializable]
[method: JsonConstructor]
public sealed record AssignmentResponse(
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
  [property: JsonPropertyName("dueOnUtc")]
  string? DueOnUtc,
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
