namespace Server.Common.Requests.Course;

/// <summary>
/// A request to update a course.
/// </summary>
/// <param name="Name">The course's name. Required.</param>
/// <param name="Code">The course's code.</param>
/// <param name="Description">The course's description.</param>
/// <param name="Image">The course's image.</param>
/// <param name="IsPublic">Whether the course is public. Required.</param>
public record UpdateRequest(
  string Name,
  string? Code,
  string? Description,
  IFormFile? Image,
  bool? IsPublic
);