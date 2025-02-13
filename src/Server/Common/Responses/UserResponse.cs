using System.Text.Json.Serialization;
using Server.Common.DTOs;

namespace Server.Common.Responses;

/// <summary>
/// Detailed user response.
/// </summary>
/// <param name="Id">The user's id.</param>
/// <param name="Email">The user's email.</param>
/// <param name="FirstName">The user's first name.</param>
/// <param name="LastName">The user's last name.</param>
/// <param name="DisplayName">The user's display name.</param>
/// <param name="Description">The user's description.</param>
/// <param name="ImageUrl">The user's image url.</param>
/// <param name="CreatedOnUtc">The user's creation date.</param>
/// <param name="DirectMessages">The user's direct messages.</param>
/// <param name="Courses">The user's joined courses.</param>
/// <param name="Organizations">The user's joined organization.</param>
[Serializable]
[method: JsonConstructor]
public record UserResponse(
  [property: JsonRequired]
  [property: JsonPropertyName("id")]
  long Id,
  [property: JsonRequired]
  [property: JsonPropertyName("email")]
  string Email,
  [property: JsonRequired]
  [property: JsonPropertyName("firstName")]
  string FirstName,
  [property: JsonRequired]
  [property: JsonPropertyName("lastName")]
  string LastName,
  [property: JsonRequired]
  [property: JsonPropertyName("displayName")]
  string DisplayName,
  [property: JsonRequired]
  [property: JsonPropertyName("description")]
  string? Description,
  [property: JsonRequired]
  [property: JsonPropertyName("imageUrl")]
  string? ImageUrl,
  [property: JsonRequired]
  [property: JsonPropertyName("createdOnUtc")]
  string CreatedOnUtc,
  [property: JsonRequired]
  [property: JsonPropertyName("directMessages")]
  IEnumerable<ChannelDto> DirectMessages,
  [property: JsonRequired]
  [property: JsonPropertyName("courses")]
  IEnumerable<CourseDto> Courses,
  [property: JsonRequired]
  [property: JsonPropertyName("organizations")]
  IEnumerable<OrganizationDto> Organizations
);
