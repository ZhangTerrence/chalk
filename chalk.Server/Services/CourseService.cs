using chalk.Server.Data;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
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
        return await _context.Courses.Select(e => e.ToResponse()).ToListAsync();
    }

    public async Task<CourseResponse> GetCourseAsync(long courseId)
    {
        var course = await _context.Courses.FindAsync(courseId);
        if (course is null)
        {
            throw new BadHttpRequestException("Course not found.", StatusCodes.Status404NotFound);
        }

        return course.ToResponse();
    }

    public async Task<CourseResponse> CreateCourseAsync(CreateCourseRequest createCourseRequest)
    {
        var organization = await _context.Organizations.FindAsync(createCourseRequest.OrganizationId);
        if (organization is null)
        {
            throw new BadHttpRequestException("Organization not found.", StatusCodes.Status404NotFound);
        }

        var course = createCourseRequest.ToEntity(organization);
        await _context.Courses.AddAsync(course);

        await _context.SaveChangesAsync();
        return course.ToResponse();
    }

    public async Task<CourseResponse> UpdateCourseAsync(long courseId, UpdateCourseRequest updateCourseRequest)
    {
        var course = await _context.Courses.FindAsync(courseId);
        if (course is null)
        {
            throw new BadHttpRequestException("Course not found.", StatusCodes.Status404NotFound);
        }

        if (updateCourseRequest.Name is not null)
        {
            course.Name = course.Name;
        }

        if (updateCourseRequest.Code is not null)
        {
            course.Code = updateCourseRequest.Code;
        }

        if (updateCourseRequest.Description is not null)
        {
            course.Description = updateCourseRequest.Description;
        }

        if (updateCourseRequest.OrganizationId is not null)
        {
            var organization = await _context.Organizations.FindAsync(updateCourseRequest.OrganizationId);
            if (organization is null)
            {
                throw new BadHttpRequestException("Organization not found.", StatusCodes.Status404NotFound);
            }

            course.Organization = organization;
        }

        await _context.SaveChangesAsync();
        return course.ToResponse();
    }

    public async Task DeleteCourseAsync(long courseId)
    {
        var course = await _context.Courses.FindAsync(courseId);
        if (course is null)
        {
            throw new BadHttpRequestException("Course not found.", StatusCodes.Status404NotFound);
        }

        _context.Courses.Remove(course);

        await _context.SaveChangesAsync();
    }
}