using chalk.Server.Configurations;
using chalk.Server.Data;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
using chalk.Server.Shared;
using chalk.Server.Utilities;
using Microsoft.EntityFrameworkCore;

namespace chalk.Server.Services;

public class CourseService : ICourseService
{
    private readonly DatabaseContext _context;

    public CourseService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CourseResponse>> GetCoursesAsync()
    {
        return await _context.Courses.Select(e => e.ToDTO()).ToListAsync();
    }

    public async Task<CourseResponse> GetCourseAsync(long courseId)
    {
        var course = await _context.Courses.FindAsync(courseId);
        if (course is null)
        {
            throw new ServiceException("Course not found.", StatusCodes.Status404NotFound);
        }

        return course.ToDTO();
    }

    public async Task<CourseResponse> CreateCourseAsync(long userId, CreateCourseRequest request)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null)
        {
            throw new ServiceException("User not found.", StatusCodes.Status404NotFound);
        }

        var course = request.ToEntity(null);
        var role = CreateRoleRequest
            .CreateCourseRole("Instructor", null, PermissionUtilities.All, 0, course.Id)
            .ToEntity(course);
        var courseUser = new UserCourse
        {
            Status = UserStatus.Joined,
            Grade = 0,
            JoinedDate = DateTime.UtcNow,
            User = user,
            Course = course,
            Role = role
        };

        var createdCourse = await _context.Courses.AddAsync(course);
        course.Users.Add(courseUser);
        course.Roles.Add(role);

        await _context.SaveChangesAsync();
        return await GetCourseAsync(createdCourse.Entity.Id);
    }

    public async Task<CourseResponse> UpdateCourseAsync(long courseId, UpdateCourseRequest request)
    {
        var course = await _context.Courses.FindAsync(courseId);
        if (course is null)
        {
            throw new ServiceException("Course not found.", StatusCodes.Status404NotFound);
        }

        if (request.Name is not null)
        {
            course.Name = request.Name;
        }

        if (request.Description is not null)
        {
            course.Description = request.Description;
        }

        if (request.PreviewImage is not null)
        {
            course.PreviewImage = request.PreviewImage;
        }

        if (request.Code is not null)
        {
            course.Code = request.Code;
        }

        if (request.Public is not null)
        {
            course.Public = request.Public!.Value;
        }

        await _context.SaveChangesAsync();
        return course.ToDTO();
    }

    public async Task DeleteCourseAsync(long courseId)
    {
        var course = await _context.Courses.FindAsync(courseId);
        if (course is null)
        {
            throw new ServiceException("Course not found.", StatusCodes.Status404NotFound);
        }

        _context.Courses.Remove(course);

        await _context.SaveChangesAsync();
    }
}