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
    private readonly ICloudService _cloudService;
    private readonly IFileService _fileService;

    public CourseService(DatabaseContext context, ICloudService cloudService, IFileService fileService)
    {
        _context = context;
        _cloudService = cloudService;
        _fileService = fileService;
    }

    public async Task<IEnumerable<Course>> GetCoursesAsync()
    {
        return await _context.Courses
            .Include(e => e.Modules).ThenInclude(e => e.Files).AsSingleQuery()
            .Include(e => e.AssignmentGroups).ThenInclude(e => e.Assignments).AsSingleQuery()
            .ToListAsync();
    }

    public async Task<Course> GetCourseAsync(long courseId)
    {
        var course = await _context.Courses
            .Include(e => e.Modules).ThenInclude(e => e.Files).AsSingleQuery()
            .Include(e => e.AssignmentGroups).ThenInclude(e => e.Assignments).AsSingleQuery()
            .FirstOrDefaultAsync(e => e.Id == courseId);
        if (course is null)
        {
            throw new ServiceException("Course not found.", StatusCodes.Status404NotFound);
        }
        return course;
    }

    public async Task<Module> GetModuleAsync(long moduleId)
    {
        var courseModule = await _context.Modules
            .Include(e => e.Files).AsSingleQuery()
            .FirstOrDefaultAsync(e => e.Id == moduleId);
        if (courseModule is null)
        {
            throw new ServiceException("Module not found.", StatusCodes.Status404NotFound);
        }
        return courseModule;
    }

    public async Task<AssignmentGroup> GetAssignmentGroupAsync(long assignmentGroupId)
    {
        var assignmentGroup = await _context.AssignmentGroups
            .Include(e => e.Assignments).AsSingleQuery()
            .FirstOrDefaultAsync(e => e.Id == assignmentGroupId);
        if (assignmentGroup is null)
        {
            throw new ServiceException("Assignment group not found.", StatusCodes.Status404NotFound);
        }
        return assignmentGroup;
    }

    public async Task<Assignment> GetAssignmentAsync(long assignmentId)
    {
        var assignment = await _context.Assignments
            .Include(e => e.Files).AsSingleQuery()
            .Include(e => e.Submissions).AsSingleQuery()
            .FirstOrDefaultAsync(e => e.Id == assignmentId);
        if (assignment is null)
        {
            throw new ServiceException("Assignment not found.", StatusCodes.Status404NotFound);
        }
        return assignment;
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
        var role = new CreateRoleRequest("Instructor", null, PermissionUtilities.All, 0)
            .ToEntity(course.Id, null);
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

    public async Task<Module> CreateModuleAsync(long courseId, CreateModuleRequest request)
    {
        var course = await GetCourseAsync(courseId);
        var module = request.ToEntity(course.Modules.Select(e => e.RelativeOrder).DefaultIfEmpty(-1).Max() + 1);
        course.Modules.Add(module);
        course.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return await GetModuleAsync(module.Id);
    }

    public async Task<AssignmentGroup> CreateAssignmentGroupAsync(long courseId, CreateAssignmentGroupRequest request)
    {
        var course = await GetCourseAsync(courseId);
        var assignmentGroup = request.ToEntity(course.Id);
        course.AssignmentGroups.Add(assignmentGroup);
        course.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return await GetAssignmentGroupAsync(assignmentGroup.Id);
    }

    public async Task<Assignment> CreateAssignmentAsync(long courseId, long assignmentGroupId, CreateAssignmentRequest request)
    {
        var course = await GetCourseAsync(courseId);
        var assignmentGroup = await GetAssignmentGroupAsync(assignmentGroupId);
        var assignment = request.ToEntity(assignmentGroup.Id);
        assignmentGroup.Assignments.Add(assignment);
        course.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return await GetAssignmentAsync(assignment.Id);
    }

    public async Task<Course> UpdateCourseAsync(long courseId, UpdateCourseRequest request)
    {
        var course = await GetCourseAsync(courseId);
        course.Name = request.Name;
        course.Code = request.Code;
        course.Description = request.Description;
        if (request.Image is not null)
        {
            course.ImageUrl = await _cloudService.UploadImageAsync(Guid.NewGuid().ToString(), request.Image);
        }
        course.IsPublic = request.IsPublic!.Value;
        course.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return await GetCourseAsync(course.Id);
    }

    public async Task<Course> ReorderModulesAsync(long courseId, ReorderModulesRequest request)
    {
        var i = 0;
        foreach (var moduleId in request.Modules)
        {
            var module = await GetModuleAsync(moduleId);
            module.RelativeOrder = i;
            i++;
        }
        await _context.SaveChangesAsync();

        return await GetCourseAsync(courseId);
    }

    public async Task<Module> UpdateModuleAsync(long courseId, long moduleId, UpdateModuleRequest request)
    {
        var module = await GetModuleAsync(moduleId);
        module.Name = request.Name;
        module.Description = request.Description;
        module.UpdatedDate = DateTime.UtcNow;
        var course = await GetCourseAsync(courseId);
        course.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return await GetModuleAsync(module.Id);
    }

    public async Task DeleteCourseAsync(long courseId)
    {
        var course = await GetCourseAsync(courseId);
        if (course.ImageUrl is not null)
        {
            await _cloudService.DeleteAsync(course.ImageUrl);
        }
        foreach (var module in course.Modules.ToList())
        {
            await DeleteModuleAsync(courseId, module.Id);
        }
        _context.Courses.Remove(course);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteModuleAsync(long courseId, long moduleId)
    {
        var module = await GetModuleAsync(moduleId);
        var course = await GetCourseAsync(courseId);
        _context.Modules.Remove(module);
        foreach (var e in course.Modules.ToList().Where(e => e.RelativeOrder > module.RelativeOrder))
        {
            e.RelativeOrder -= 1;
        }
        foreach (var file in module.Files.ToList())
        {
            await _fileService.DeleteFile(file.Id);
        }
        course.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }
}