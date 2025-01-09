namespace chalk.Server.DTOs.Requests;

public record CreateCourseRequest(
    string? Name,
    string? Description,
    string? Code,
    bool? Public
);