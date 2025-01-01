namespace chalk.Server.DTOs.Requests;

public record UpdateCourseRequest(string? Name, string? Code, string? Description, long? OrganizationId);