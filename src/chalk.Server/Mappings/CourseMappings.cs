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
            Name = request.Name!,
            Description = request.Description,
            Code = request.Code,
            Public = request.Public!.Value,
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
            course.Description,
            course.PreviewImage,
            course.Code,
            course.Public,
            course.CreatedDate.ToString(CultureInfo.CurrentCulture),
            course.UpdatedDate.ToString(CultureInfo.CurrentCulture),
            course.Organization?.ToDTO(),
            course.Users.Select(e => e.User.ToDTO(e.JoinedDate?.ToString(CultureInfo.CurrentCulture))),
            course.Roles.Select(e => e.ToDTO())
        );
    }

    public static CourseDTO ToDTO(this Course course)
    {
        return new CourseDTO(course.Id, course.Name, course.Name);
    }

    public static CourseRole ToEntity(this CreateRoleRequest request, Course course)
    {
        return new CourseRole
        {
            Name = request.Name!,
            Description = request.Description,
            Permissions = request.Permissions!.Value,
            Rank = request.Rank!.Value,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Course = course
        };
    }

    public static RoleResponse ToResponse(this CourseRole courseRole)
    {
        return new RoleResponse(
            courseRole.Id,
            courseRole.Name,
            courseRole.Description,
            courseRole.Permissions,
            courseRole.Rank,
            courseRole.CreatedDate.ToString(CultureInfo.CurrentCulture),
            courseRole.UpdatedDate.ToString(CultureInfo.CurrentCulture)
        );
    }

    public static RoleDTO ToDTO(this CourseRole courseRole)
    {
        return new RoleDTO(courseRole.Id, courseRole.Name, courseRole.Permissions, courseRole.Rank);
    }
}