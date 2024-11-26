using chalk.Server.Data;
using chalk.Server.DTOs.Course;
using chalk.Server.Extensions;
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

    public async Task<IEnumerable<CourseDTO>> GetCoursesAsync()
    {
        return await _context.Courses.Select(e => e.ToCourseDTO()).ToListAsync();
    }

    public async Task<CourseDTO> GetCourseAsync(Guid courseId)
    {
        var course = await _context.Courses.FindAsync(courseId);
        if (course is null)
        {
            throw new BadHttpRequestException("Course not found.", StatusCodes.Status404NotFound);
        }

        return course.ToCourseDTO();
    }

    public async Task<CourseDTO> CreateCourseAsync(CreateCourseDTO createCourseDto)
    {
        var organization = await _context.Organizations.FindAsync(createCourseDto.OrganizationId);
        if (organization is null)
        {
            throw new BadHttpRequestException("Organization not found.", StatusCodes.Status404NotFound);
        }

        var course = createCourseDto.ToCourse(organization);
        await _context.Courses.AddAsync(course);

        await _context.SaveChangesAsync();
        return course.ToCourseDTO();
    }

    public async Task<CourseDTO> UpdateCourseAsync(Guid courseId, UpdateCourseDTO updateCourseDTO)
    {
        var course = await _context.Courses.FindAsync(courseId);
        if (course is null)
        {
            throw new BadHttpRequestException("Course not found.", StatusCodes.Status404NotFound);
        }

        if (updateCourseDTO.Name is not null)
        {
            course.Name = course.Name;
        }

        if (updateCourseDTO.Code is not null)
        {
            course.Code = updateCourseDTO.Code;
        }

        if (updateCourseDTO.Description is not null)
        {
            course.Description = updateCourseDTO.Description;
        }

        if (updateCourseDTO.OrganizationId is not null)
        {
            var organization = await _context.Organizations.FindAsync(updateCourseDTO.OrganizationId);
            if (organization is null)
            {
                throw new BadHttpRequestException("Organization not found.", StatusCodes.Status404NotFound);
            }

            course.Organization = organization;
        }

        await _context.SaveChangesAsync();
        return course.ToCourseDTO();
    }

    public async Task DeleteCourseAsync(Guid courseId)
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