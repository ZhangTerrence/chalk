using chalk.Server.Common;
using chalk.Server.DTOs.Course;
using chalk.Server.DTOs.User;
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

    public static InviteDTO ToInviteDTO(this UserCourse userCourse)
    {
        return new InviteDTO(InviteType.Course, null, userCourse.Course);
    }

    public static CourseDTO ToCourseDTO(this Course course)
    {
        return new CourseDTO(course);
    }
}