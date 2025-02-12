namespace Server.Common.Requests.Course;

/// <summary>
/// A request to create a course.
/// </summary>
/// <param name="Name">The course's name. Required.</param>
/// <param name="Code">The course's code.</param>
/// <param name="Description">The course's description.</param>
/// <param name="IsPublic">Whether the course is public. Required.</param>
public record CreateRequest(
  string Name,
  string? Code,
  string? Description,
  bool? IsPublic
);