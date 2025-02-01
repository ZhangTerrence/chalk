namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to update a course.
/// </summary>
/// <param name="Name">The course's name.</param>
/// <param name="Code">The course's code.</param>
/// <param name="Description">The course's description.</param>
/// <param name="Image">The course's image.</param>
/// <param name="IsPublic">Whether the course is public.</param>
/// <remarks><paramref name="Name"/> and <paramref name="IsPublic"/> are required.</remarks>
public record UpdateCourseRequest(
    string Name,
    string? Code,
    string? Description,
    IFormFile? Image,
    bool? IsPublic
);