namespace chalk.Server.DTOs;

public record UpdateCourseDTO(string Name, string? Code, string? Description, long? OrganizationId)
{
}