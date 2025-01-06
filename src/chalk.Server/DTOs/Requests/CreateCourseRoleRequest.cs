namespace chalk.Server.DTOs.Requests;

public record CreateCourseRoleRequest(
    string? Name,
    string? Description,
    long? Permissions,
    int? Rank,
    long? CourseId
);