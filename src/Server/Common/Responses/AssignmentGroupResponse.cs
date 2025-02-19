using System.Text.Json.Serialization;

namespace Server.Common.Responses;

/// <summary>
/// Assignment group response.
/// </summary>
/// <param name="Id">The assignment group's id.</param>
/// <param name="Name">The assignment group's name.</param>
/// <param name="Description">The assignment group's description.</param>
/// <param name="Weight">The assignment group's weight.</param>
/// <param name="CreatedOnUtc">The assignment group's creation date.</param>
/// <param name="UpdatedOnUtc">The assignment group's updated date.</param>
/// <param name="Assignments">The assignment group's assignments.</param>
[Serializable]
[method: JsonConstructor]
public sealed record AssignmentGroupResponse(
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
  [property: JsonPropertyName("weight")]
  int Weight,
  [property: JsonRequired]
  [property: JsonPropertyName("createdOnUtc")]
  string CreatedOnUtc,
  [property: JsonRequired]
  [property: JsonPropertyName("updatedOnUtc")]
  string UpdatedOnUtc,
  [property: JsonRequired]
  [property: JsonPropertyName("assignments")]
  IEnumerable<AssignmentResponse> Assignments
);
