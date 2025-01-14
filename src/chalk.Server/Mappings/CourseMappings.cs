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
            course.Image,
            course.IsPublic,
            course.CreatedDate.ToString(CultureInfo.CurrentCulture),
            course.Organization?.ToDTO(),
            course.Users.Select(e => e.User.ToDTO(e.JoinedDate?.ToString(CultureInfo.CurrentCulture))),
            course.Roles.Select(e => e.ToDTO())
        );
    }

    public static CourseDTO ToDTO(this Course course)
    {
        return new CourseDTO(
            course.Id,
            course.Name,
            course.Code,
            course.Description,
            course.Image,
            course.CreatedDate.ToString(CultureInfo.CurrentCulture)
        );
    }
}