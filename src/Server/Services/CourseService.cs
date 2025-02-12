using Microsoft.EntityFrameworkCore;
using Server.Common.Enums;
using Server.Common.Exceptions;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.Course;
using Server.Common.Utilities;
using Server.Data;
using Server.Data.Entities;
using CreateRoleRequest = Server.Common.Requests.Role.CreateRequest;

namespace Server.Services;

public class CourseService : ICourseService
{
  private readonly IAssignmentGroupService _assignmentGroupService;
  private readonly ICloudService _cloudService;
  private readonly DatabaseContext _context;
  private readonly IModuleService _moduleService;

  public CourseService(
    ICloudService cloudService,
    DatabaseContext context,
    IModuleService moduleService,
    IAssignmentGroupService assignmentGroupService
  )
  {
    this._cloudService = cloudService;
    this._context = context;
    this._moduleService = moduleService;
    this._assignmentGroupService = assignmentGroupService;
  }

  public async Task<IEnumerable<Course>> GetCoursesAsync(long requesterId)
  {
    return await this._context.Courses
      .Include(e => e.Modules).ThenInclude(e => e.Files).AsSingleQuery()
      .Include(e => e.AssignmentGroups).ThenInclude(e => e.Assignments).AsSingleQuery()
      .ToListAsync();
  }


  public async Task<Course> GetCourseByIdAsync(long courseId, long requesterId)
  {
    var course = await this._context.Courses
      .Include(e => e.Modules).ThenInclude(e => e.Files).AsSingleQuery()
      .Include(e => e.AssignmentGroups).ThenInclude(e => e.Assignments).ThenInclude(e => e.Files).AsSingleQuery()
      .Include(e => e.AssignmentGroups).ThenInclude(e => e.Assignments).ThenInclude(e => e.Submissions).AsSingleQuery()
      .FirstOrDefaultAsync(e => e.Id == courseId);
    if (course is null) ServiceException.NotFound("Course not found.", course);

    return course;
  }

  public async Task<Course> CreateCourseAsync(long requesterId, CreateRequest request)
  {
    var user = await this._context.Users.FindAsync(requesterId);
    if (user is null) ServiceException.NotFound("User not found.", user);

    var course = request.ToEntity(null);
    var createdCourse = await this._context.Courses.AddAsync(course);
    var role = new CreateRoleRequest("Instructor", null, PermissionUtilities.All, 0)
      .ToEntity(course.Id, null);
    var userCourse = new UserCourse
    {
      Status = UserStatus.Joined,
      JoinedOnUtc = DateTime.UtcNow,
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

    await this._context.SaveChangesAsync();
    return await this.GetCourseByIdAsync(createdCourse.Entity.Id, requesterId);
  }

  public async Task<Course> UpdateCourseAsync(long courseId, long requesterId, UpdateRequest request)
  {
    var course = await this.GetCourseByIdAsync(courseId, requesterId);

    course.Name = request.Name;
    course.Code = request.Code;
    course.Description = request.Description;
    if (request.Image is not null)
      course.ImageUrl = await this._cloudService.UploadImageAsync(Guid.NewGuid().ToString(), request.Image);
    course.IsPublic = request.IsPublic!.Value;
    course.UpdatedOnUtc = DateTime.UtcNow;

    await this._context.SaveChangesAsync();
    return await this.GetCourseByIdAsync(courseId, requesterId);
  }

  public async Task DeleteCourseAsync(long courseId, long requesterId)
  {
    var course = await this.GetCourseByIdAsync(courseId, requesterId);

    if (course.ImageUrl is not null) await this._cloudService.DeleteAsync(course.ImageUrl);
    foreach (var module in course.Modules.ToList()) await this._moduleService.DeleteModuleAsync(module.Id, requesterId);
    foreach (var assignmentGroup in course.AssignmentGroups.ToList())
      await this._assignmentGroupService.DeleteAssignmentGroupAsync(assignmentGroup.Id, requesterId);

    this._context.Courses.Remove(course);
    await this._context.SaveChangesAsync();
  }
}
