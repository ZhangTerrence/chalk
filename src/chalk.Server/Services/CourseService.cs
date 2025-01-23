using chalk.Server.Configurations;
using chalk.Server.Data;
using chalk.Server.DTOs.Requests;
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
    private readonly IFileUploadService _fileUploadService;

    public CourseService(DatabaseContext context, IFileUploadService fileUploadService)
    {
        _context = context;
        _fileUploadService = fileUploadService;
    }

    public async Task<IEnumerable<Course>> GetCoursesAsync()
    {
        return await _context.Courses
            .Include(e => e.Organization).AsSingleQuery()
            .Include(e => e.Users).ThenInclude(u => u.User).AsSingleQuery()
            .Include(e => e.Roles).AsSingleQuery()
            .Include(e => e.Modules).ThenInclude(e => e.Attachments).AsSingleQuery()
            .ToListAsync();
    }

    public async Task<Course> GetCourseAsync(long courseId)
    {
        var course = await _context.Courses
            .Include(e => e.Organization).AsSingleQuery()
            .Include(e => e.Users).ThenInclude(u => u.User).AsSingleQuery()
            .Include(e => e.Roles).AsSingleQuery()
            .Include(e => e.Modules).ThenInclude(e => e.Attachments).AsSingleQuery()
            .FirstOrDefaultAsync(e => e.Id == courseId);
        if (course is null)
        {
            throw new ServiceException("Course not found.", StatusCodes.Status404NotFound);
        }

        return course;
    }

    public async Task<Course> CreateCourseAsync(long userId, CreateCourseRequest request)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null)
        {
            throw new ServiceException("User not found.", StatusCodes.Status404NotFound);
        }

        var course = request.ToEntity(null);
        var createdCourse = await _context.Courses.AddAsync(course);

        var role = CreateRoleRequest
            .CreateCourseRole("Instructor", null, PermissionUtilities.All, 0, course.Id)
            .ToEntity();

        var userCourse = new UserCourse
        {
            Status = UserStatus.Joined,
            JoinedDate = DateTime.UtcNow,
            User = user,
            Course = course
        };
        var userRole = new UserRole
        {
            UserCourse = userCourse,
            Role = role
        };
        userCourse.Roles.Add(userRole);

        course.Users.Add(userCourse);
        course.Roles.Add(role);

        await _context.SaveChangesAsync();
        return await GetCourseAsync(createdCourse.Entity.Id);
    }

    public async Task<Course> UpdateCourseAsync(long courseId, UpdateCourseRequest request)
    {
        var course = await _context.Courses.FindAsync(courseId);
        if (course is null)
        {
            throw new ServiceException("Course not found.", StatusCodes.Status404NotFound);
        }

        course.Name = request.Name;
        course.Code = request.Code;
        course.Description = request.Description;
        if (request.Image is not null)
        {
            var hash = FileUtilities.S3ObjectHash("course-image", course.Id.ToString());
            var uri = await _fileUploadService.UploadAsync(hash, request.Image);
            course.Image = uri;
        }

        course.IsPublic = request.IsPublic!.Value;
        var i = 0;
        foreach (var courseModuleId in request.Modules)
        {
            var courseModule = await _context.CourseModules.FindAsync(courseModuleId);
            if (courseModule is null)
            {
                throw new ServiceException("Course module not found.", StatusCodes.Status404NotFound);
            }

            courseModule.RelativeOrder = i;
            i++;
        }

        course.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return await GetCourseAsync(course.Id);
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

    public async Task<Course> CreateCourseModuleAsync(CreateCourseModuleRequest request)
    {
        var course = await _context.Courses
            .Include(e => e.Modules)
            .FirstOrDefaultAsync(e => e.Id == request.CourseId);
        if (course is null)
        {
            throw new ServiceException("Course not found.", StatusCodes.Status404NotFound);
        }

        // Adds to the end
        var relativeOrder = course.Modules.Select(e => e.RelativeOrder).DefaultIfEmpty(0).Max();

        var courseModule = request.ToEntity(relativeOrder);
        course.Modules.Add(courseModule);

        await _context.SaveChangesAsync();
        return await GetCourseAsync(course.Id);
    }

    public async Task<Course> UpdateCourseModuleAsync(long courseModuleId, UpdateCourseModuleRequest request)
    {
        var courseModule = await _context.CourseModules.FindAsync(courseModuleId);
        if (courseModule is null)
        {
            throw new ServiceException("Course module not found.", StatusCodes.Status404NotFound);
        }

        courseModule.Name = request.Name;
        courseModule.Description = request.Description;
        courseModule.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        var course = await _context.Courses.FindAsync(courseModule.CourseId);
        if (course is null)
        {
            throw new ServiceException("Course not found.", StatusCodes.Status404NotFound);
        }

        return await GetCourseAsync(course.Id);
    }

    public async Task<Course> DeleteCourseModuleAsync(long courseModuleId)
    {
        var courseModule = await _context.CourseModules.FindAsync(courseModuleId);
        if (courseModule is null)
        {
            throw new ServiceException("Course module not found.", StatusCodes.Status404NotFound);
        }

        var course = await _context.Courses
            .Include(e => e.Modules)
            .FirstOrDefaultAsync(e => e.Id == courseModule.CourseId);
        if (course is null)
        {
            throw new ServiceException("Course not found.", StatusCodes.Status404NotFound);
        }

        _context.CourseModules.Remove(courseModule);

        foreach (var module in course.Modules)
        {
            module.RelativeOrder -= 1;
        }

        await _context.SaveChangesAsync();

        return await GetCourseAsync(course.Id);
    }
}