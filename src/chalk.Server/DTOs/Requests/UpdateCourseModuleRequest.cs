namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to update a course module.
/// </summary>
/// <param name="Name">The course module's name.</param>
/// <param name="Description">The course module's description.</param>
/// <remarks><paramref name="Name"/> is required.</remarks>
public record UpdateCourseModuleRequest(
    string Name,
    string? Description
);