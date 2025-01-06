using System.Globalization;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;
using chalk.Server.Utilities;

namespace chalk.Server.Mappings;

public static class CourseMappings
{
    public static Course ToEntity(this CreateCourseRequest request, Organization? organization)
    {
        return new Course
        {
            Name = request.Name!,
            Description = request.Description,
            PreviewImage = request.PreviewImage,
            Code = request.Code,
            Public = request.Public!.Value,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Organization = organization,
        };
    }

    public static CourseResponse ToDTO(this Course course)
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
            course.Organization is not null ? new OrganizationDTO(course.Organization) : null,
            course.Users
                .Select(e => new UserDTO(e.User, e.JoinedDate?.ToString(CultureInfo.CurrentCulture)))
                .ToList(),
            course.Roles
                .Select(e => new CourseRoleDTO(e))
                .ToList()
        );
    }

    public static CourseRole ToEntity(this CreateCourseRoleRequest request)
    {
        return new CourseRole
        {
            Name = request.Name!,
            Description = request.Description,
            Permissions = request.Permissions!.Value,
            Rank = request.Rank!.Value,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
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
}