using Microsoft.EntityFrameworkCore;
using Server.Common.Exceptions;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.Assignment;
using Server.Data;
using Server.Data.Entities;

namespace Server.Services;

internal class AssignmentService : IAssignmentService
{
  private readonly DatabaseContext _context;
  private readonly IFileService _fileService;

  public AssignmentService(DatabaseContext context, IFileService fileService)
  {
    this._context = context;
    this._fileService = fileService;
  }

  public async Task<Assignment> GetAssignmentByIdAsync(long assignmentId, long requesterId)
  {
    var assignment = await this._context.Assignments
      .Include(e => e.Files).AsSingleQuery()
      .Include(e => e.Submissions).AsSingleQuery()
      .FirstOrDefaultAsync(e => e.Id == assignmentId);
    if (assignment is null) ServiceException.NotFound("Assignment not found.", assignment);

    return assignment;
  }

  public async Task<Assignment> CreateAssignmentAsync(long requesterId, CreateRequest request)
  {
    var assignment = request.ToEntity(request.AssignmentGroupId!.Value);
    var assignmentGroup = await this._context.AssignmentGroups.FindAsync(assignment.AssignmentGroupId);
    if (assignmentGroup is null) ServiceException.NotFound("Assignment group not found.", assignmentGroup);
    var course = await this._context.Courses
      .Include(e => e.AssignmentGroups)
      .FirstOrDefaultAsync(e => e.Id == assignmentGroup.CourseId);
    if (course is null) ServiceException.NotFound("Course not found.", course);

    var aggregateWeight = course.AssignmentGroups.Aggregate(0, (n, e) => n + e.Weight);
    if (aggregateWeight != 100)
      ServiceException.BadRequest("Sum of assignment group weights must be 100%.");
    assignmentGroup.Assignments.Add(assignment);
    course.UpdatedOnUtc = DateTime.UtcNow;

    await this._context.SaveChangesAsync();
    return await this.GetAssignmentByIdAsync(assignment.Id, requesterId);
  }

  public async Task<Assignment> UpdateAssignmentAsync(long assignmentId, long requesterId, UpdateRequest request)
  {
    var assignment = await this.GetAssignmentByIdAsync(assignmentId, requesterId);
    var assignmentGroup = await this._context.AssignmentGroups.FindAsync(assignment.AssignmentGroupId);
    if (assignmentGroup is null) ServiceException.NotFound("Assignment group not found.", assignmentGroup);
    var course = await this._context.Courses.FindAsync(assignmentGroup.CourseId);
    if (course is null) ServiceException.NotFound("Course not found.", course);

    assignment.Name = request.Name;
    assignment.Description = request.Description;
    assignment.DueOnUtc = request.DueOnUtc;
    assignment.UpdatedOnUtc = DateTime.UtcNow;
    assignmentGroup.UpdatedOnUtc = DateTime.UtcNow;
    course.UpdatedOnUtc = DateTime.UtcNow;

    await this._context.SaveChangesAsync();
    return await this.GetAssignmentByIdAsync(assignmentId, requesterId);
  }

  public async Task DeleteAssignmentAsync(long assignmentId, long requesterId)
  {
    var assignment = await this.GetAssignmentByIdAsync(assignmentId, requesterId);
    var assignmentGroup = await this._context.AssignmentGroups.FindAsync(assignment.AssignmentGroupId);
    if (assignmentGroup is null) ServiceException.NotFound("Assignment group not found.", assignmentGroup);
    var course = await this._context.Courses.FindAsync(assignmentGroup.CourseId);
    if (course is null) ServiceException.NotFound("Course not found.", course);

    this._context.Assignments.Remove(assignment);
    foreach (var file in assignment.Files.ToList()) await this._fileService.DeleteFile(file.Id, requesterId);
    // TODO - Delete submissions
    assignmentGroup.UpdatedOnUtc = DateTime.UtcNow;
    course.UpdatedOnUtc = DateTime.UtcNow;

    await this._context.SaveChangesAsync();
  }
}
