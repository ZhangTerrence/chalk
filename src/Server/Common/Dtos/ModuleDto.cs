using System.Text.Json.Serialization;
using Server.Controllers;

namespace Server.Common.DTOs;

[Serializable]
[method: JsonConstructor]
public sealed record ModuleDto(
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
  [property: JsonPropertyName("files")]
  IEnumerable<FileDto> Files
) : FileController.FileResponse;
