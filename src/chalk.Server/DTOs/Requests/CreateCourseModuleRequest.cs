namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to create a course module.
/// </summary>
/// <param name="Name">The course module's name.</param>
/// <param name="Description">The course module's description.</param>
/// <param name="CourseId">The course the module belongs to.</param>
/// <remarks><paramref name="Name"/> and <paramref name="CourseId"/> are required.</remarks>
public record CreateCourseModuleRequest(
    string Name,
    string? Description,
    long? CourseId
);