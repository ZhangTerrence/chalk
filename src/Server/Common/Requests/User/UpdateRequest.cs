namespace Server.Common.Requests.User;

/// <summary>
/// A request to update a user's profile.
/// </summary>
/// <param name="FirstName">The user's first name. Required.</param>
/// <param name="LastName">The user's last name. Required.</param>
/// <param name="DisplayName">The user's display name. Required.</param>
/// <param name="Description">The user's description.</param>
/// <param name="Image">The user's image.</param>
public record UpdateRequest(
  string FirstName,
  string LastName,
  string DisplayName,
  string? Description,
  IFormFile? Image
);