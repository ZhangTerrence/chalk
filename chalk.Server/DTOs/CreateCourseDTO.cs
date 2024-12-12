namespace chalk.Server.DTOs;

public record CreateCourseDTO(string Name, string? Code, string? Description, long OrganizationId)
{
}