using System.Text.Json.Serialization;

namespace Server.Common.Responses;

/// <summary>
/// User response.
/// </summary>
/// <param name="Id">The user's id.</param>
/// <param name="Email">The user's email.</param>
/// <param name="FirstName">The user's first name.</param>
/// <param name="LastName">The user's last name.</param>
/// <param name="DisplayName">The user's display name.</param>
/// <param name="Description">The user's description.</param>
/// <param name="ImageUrl">The user's image url.</param>
/// <param name="CreatedOnUtc">The user's creation date.</param>
/// <param name="UpdatedOnUtc">The user's updated date.</param>
/// <param name="DirectMessages">The user's direct messages.</param>
/// <param name="Courses">The user's courses.</param>
/// <param name="Organizations">The user's joined organization.</param>
[Serializable]
[method: JsonConstructor]
public sealed record UserResponse(
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
  [property: JsonPropertyName("updatedOnUtc")]
  string UpdatedOnUtc,
  [property: JsonRequired]
  [property: JsonPropertyName("directMessages")]
  IEnumerable<UserResponse.PartialChannelResponse> DirectMessages,
  [property: JsonRequired]
  [property: JsonPropertyName("courses")]
  IEnumerable<UserResponse.PartialCourseResponse> Courses,
  [property: JsonRequired]
  [property: JsonPropertyName("organizations")]
  IEnumerable<UserResponse.PartialOrganizationResponse> Organizations
)
{
  /// <summary>
  /// Partial channel response returned alongside users.
  /// </summary>
  /// <param name="Id">The channel's id.</param>
  [Serializable]
  [method: JsonConstructor]
  public sealed record PartialChannelResponse(
    [property: JsonRequired]
    [property: JsonPropertyName("id")]
    long Id
  );

  /// <summary>
  /// Partial course response returned alongside users.
  /// </summary>
  /// <param name="Id">The course's id.</param>
  /// <param name="Name">The course's name.</param>
  /// <param name="Code">The course's code.</param>
  [Serializable]
  [method: JsonConstructor]
  public sealed record PartialCourseResponse(
    [property: JsonRequired]
    [property: JsonPropertyName("id")]
    long Id,
    [property: JsonRequired]
    [property: JsonPropertyName("name")]
    string Name,
    [property: JsonRequired]
    [property: JsonPropertyName("code")]
    string? Code
  );

  /// <summary>
  /// Partial organization response returned alongside users.
  /// </summary>
  /// <param name="Id">The organization's id.</param>
  /// <param name="Name">The organization's name.</param>
  [Serializable]
  [method: JsonConstructor]
  public sealed record PartialOrganizationResponse(
    [property: JsonRequired]
    [property: JsonPropertyName("id")]
    long Id,
    [property: JsonRequired]
    [property: JsonPropertyName("name")]
    string Name
  );
}
