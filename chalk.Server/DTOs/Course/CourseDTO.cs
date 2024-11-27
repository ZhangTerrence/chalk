namespace chalk.Server.DTOs.Course;

public record CourseDTO
{
    public CourseDTO(Entities.Course course)
    {
        Id = course.Id;
        Name = course.Name;
        Code = course.Code;
        Description = course.Description;
        OrganizationId = course.OrganizationId;
        CreatedDate = course.CreatedDate;
        UpdatedDate = course.UpdatedDate;
    }

    public long Id { get; init; }
    public string Name { get; init; }
    public string? Code { get; init; }
    public string? Description { get; init; }
    public long OrganizationId { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime UpdatedDate { get; init; }
}