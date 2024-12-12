using System.Globalization;
using chalk.Server.Common;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;

namespace chalk.Server.Extensions;

public static class CourseExtensions
{
    public static Course ToCourse(this CreateCourseDTO createCourseDTO, Organization organization)
    {
        return new Course
        {
            Name = createCourseDTO.Name,
            Code = createCourseDTO.Code,
            Description = createCourseDTO.Description,
            Organization = organization,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
    }

    public static InviteResponseDTO ToInviteResponseDTO(this UserCourse userCourse)
    {
        return new InviteResponseDTO(
            InviteType.Course,
            null,
            new InviteResponseDTO.CourseDTO(userCourse.Course.Id, userCourse.Course.Name, userCourse.Course.Code)
        );
    }

    public static CourseResponseDTO ToCourseResponseDTO(this Course course)
    {
        return new CourseResponseDTO(
            course.Id,
            course.Name,
            course.Code,
            course.Description,
            course.CreatedDate.ToString(CultureInfo.CurrentCulture),
            course.UpdatedDate.ToString(CultureInfo.CurrentCulture),
            new CourseResponseDTO.OrganizationDTO(course.Organization.Id, course.Organization.Name),
            course.UserCourses
                .Select(e => new CourseResponseDTO.UserDTO(
                    e.User.Id,
                    e.User.DisplayName,
                    e.JoinedDate?.ToString(CultureInfo.CurrentCulture)
                ))
                .ToList(),
            course.CourseRoles
                .Select(e => new CourseResponseDTO.CourseRoleDTO(e.Id, e.Name, e.Permissions))
                .ToList()
        );
    }
}