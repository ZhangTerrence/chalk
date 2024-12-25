namespace chalk.Server.DTOs.Requests;

public record CreateCourseRequest(string Name, string? Code, string? Description, long OrganizationId);