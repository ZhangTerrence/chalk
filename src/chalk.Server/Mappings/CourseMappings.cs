using System.Globalization;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;
using chalk.Server.Shared;

namespace chalk.Server.Mappings;

public static class CourseMappings
{
    public static Course ToEntity(this CreateCourseRequest createCourseRequest, Organization organization)
    {
        return new Course
        {
            Name = createCourseRequest.Name,
            Code = createCourseRequest.Code,
            Description = createCourseRequest.Description,
            Organization = organization,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
    }

    public static InviteResponse ToResponse(this UserCourse userCourse)
    {
        return new InviteResponse(
            Invite.Course,
            null,
            new CourseDTO(userCourse.Course.Id, userCourse.Course.Name, userCourse.Course.Code)
        );
    }

    public static CourseResponse ToResponse(this Course course)
    {
        return new CourseResponse(
            course.Id,
            course.Name,
            course.Code,
            course.Description,
            course.CreatedDate.ToString(CultureInfo.CurrentCulture),
            course.UpdatedDate.ToString(CultureInfo.CurrentCulture),
            new OrganizationDTO(course.Organization.Id, course.Organization.Name),
            course.UserCourses
                .Select(e => new UserDTO(
                    e.User.Id,
                    e.User.DisplayName,
                    e.JoinedDate?.ToString(CultureInfo.CurrentCulture)
                ))
                .ToList(),
            course.CourseRoles
                .Select(e => new CourseRoleDTO(e.Id, e.Name, e.Permissions))
                .ToList()
        );
    }
}