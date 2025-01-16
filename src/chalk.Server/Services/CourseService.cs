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
            .ToListAsync();
    }

    public async Task<Course> GetCourseAsync(long courseId)
    {
        var course = await _context.Courses
            .Include(e => e.Organization).AsSingleQuery()
            .Include(e => e.Users).ThenInclude(u => u.User).AsSingleQuery()
            .Include(e => e.Roles).AsSingleQuery()
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

        var createdCourse = await _context.Courses.AddAsync(course);
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
        else
        {
            course.Image = null;
        }

        course.IsPublic = request.IsPublic;

        course.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return await GetCourseAsync(courseId);
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