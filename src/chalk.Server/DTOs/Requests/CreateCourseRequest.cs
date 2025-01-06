namespace chalk.Server.DTOs.Requests;

public record CreateCourseRequest(
    string? Name,
    string? Description,
    string? PreviewImage,
    string? Code,
    bool? Public,
    long? OrganizationId
);