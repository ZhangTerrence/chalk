using chalk.Server.DTOs.Course;
using chalk.Server.Extensions;

namespace chalk.Server.DTOs.Organization;

public class OrganizationDTO
{
    public OrganizationDTO(Entities.Organization organization)
    {
        Id = organization.Id;
        Name = organization.Name;
        Description = organization.Description;
        OwnerId = organization.OwnerId;
        CreatedDate = organization.CreatedDate;
        UpdatedDate = organization.UpdatedDate;
        Courses = organization.Courses.Select(e => e.ToCourseDTO()).ToList();
    }

    public long Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public long OwnerId { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime UpdatedDate { get; init; }
    public ICollection<CourseDTO> Courses { get; init; }
}