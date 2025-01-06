namespace chalk.Server.DTOs.Requests;

public record UpdateCourseRequest(
    string? Name,
    string? Description,
    string? PreviewImage,
    string? Code,
    bool? Public
);