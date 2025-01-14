namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to update a course.
/// </summary>
/// <param name="Name">The course's name.</param>
/// <param name="Code">The course's code.</param>
/// <param name="Description">The course's description.</param>
/// <param name="Image">The course's image.</param>
/// <param name="IsPublic">Whether the course is public.</param>
/// <remarks>All properties are optional.</remarks>
public record UpdateCourseRequest(
    string? Name,
    string? Code,
    string? Description,
    string? Image,
    bool? IsPublic
);