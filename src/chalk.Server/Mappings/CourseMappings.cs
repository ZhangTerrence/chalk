using System.Globalization;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;

namespace chalk.Server.Mappings;

public static class CourseMappings
{
    public static Course ToEntity(this CreateCourseRequest request, Organization? organization)
    {
        return new Course
        {
            Name = request.Name,
            Code = request.Code,
            Description = request.Description,
            IsPublic = request.IsPublic!.Value,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Organization = organization
        };
    }

    public static CourseResponse ToResponse(this Course course)
    {
        return new CourseResponse(
            course.Id,
            course.Name,
            course.Code,
            course.Description,
            course.ImageUrl,
            course.IsPublic,
            course.CreatedDate.ToString(CultureInfo.CurrentCulture),
            course.Modules.Select(e => e.ToDTO()),
            course.AssignmentGroups.Select(e => e.ToDTO())
        );
    }

    public static CourseDTO ToDTO(this Course course)
    {
        return new CourseDTO(
            course.Id,
            course.Name,
            course.Code,
            course.Description,
            course.ImageUrl,
            course.IsPublic,
            course.CreatedDate.ToString(CultureInfo.CurrentCulture)
        );
    }
}