using System.Text.Json.Serialization;

namespace Server.Common.DTOs;

[Serializable]
[method: JsonConstructor]
public sealed record ErrorDto(
  [property: JsonRequired]
  [property: JsonPropertyName("message")]
  string Message
);
