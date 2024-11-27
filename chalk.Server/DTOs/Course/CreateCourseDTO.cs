namespace chalk.Server.DTOs.Course;

public record CreateCourseDTO
{
    public CreateCourseDTO(string name, string code, string? description, long organizationId)
    {
        Name = name;
        Code = code;
        Description = description;
        OrganizationId = organizationId;
    }

    public string Name { get; init; }
    public string Code { get; init; }
    public string? Description { get; init; }
    public long OrganizationId { get; init; }
}