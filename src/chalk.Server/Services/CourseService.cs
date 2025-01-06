using chalk.Server.Configurations;
using chalk.Server.Data;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
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

    public async Task<CourseResponse> CreateCourseAsync(string userId, CreateCourseRequest request)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null)
        {
            throw new ServiceException("User not found.", StatusCodes.Status404NotFound);
        }

        var organization = await _context.Organizations.FindAsync(request.OrganizationId);

        var course = request.ToEntity(organization);

        var role = CreateRoleRequest
            .CreateCourseRole("Instructor", null, PermissionUtilities.All, 0, course.Id)
            .ToEntity(course);

        await _context.Courses.AddAsync(course);
        await _context.CourseRoles.AddAsync(role);

        await _context.SaveChangesAsync();
        return course.ToDTO();
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
            course.Name = course.Name;
        }

        if (request.Code is not null)
        {
            course.Code = request.Code;
        }

        if (request.Description is not null)
        {
            course.Description = request.Description;
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