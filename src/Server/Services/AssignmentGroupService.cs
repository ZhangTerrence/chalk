using Microsoft.EntityFrameworkCore;
using Server.Common.Exceptions;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.AssignmentGroup;
using Server.Data;
using Server.Data.Entities;

namespace Server.Services;

public class AssignmentGroupService : IAssignmentGroupService
{
  private readonly IAssignmentService _assignmentService;
  private readonly DatabaseContext _context;

  public AssignmentGroupService(DatabaseContext context, IAssignmentService assignmentService)
  {
    this._context = context;
    this._assignmentService = assignmentService;
  }

  public async Task<AssignmentGroup> GetAssignmentGroupByIdAsync(long assignmentGroupId, long requesterId)
  {
    var assignmentGroup = await this._context.AssignmentGroups
      .Include(e => e.Assignments).ThenInclude(e => e.Files).AsSingleQuery()
      .Include(e => e.Assignments).ThenInclude(e => e.Submissions).AsSingleQuery()
      .FirstOrDefaultAsync(e => e.Id == assignmentGroupId);
    if (assignmentGroup is null) ServiceException.NotFound("Assignment group not found.", assignmentGroup);

    return assignmentGroup;
  }

  public async Task<AssignmentGroup> CreateAssignmentGroupAsync(long requesterId, CreateRequest request)
  {
    var assignmentGroup = request.ToEntity(request.CourseId!.Value);
    var course = await this._context.Courses.FindAsync(request.CourseId!.Value);
    if (course is null) ServiceException.NotFound("Course not found.", course);

    course.AssignmentGroups.Add(assignmentGroup);
    course.UpdatedOnUtc = DateTime.UtcNow;

    await this._context.SaveChangesAsync();
    return await this.GetAssignmentGroupByIdAsync(assignmentGroup.Id, requesterId);
  }

  public async Task<AssignmentGroup> UpdateAssignmentGroupAsync(long assignmentGroupId, long requesterId,
    UpdateRequest request)
  {
    var assignmentGroup = await this.GetAssignmentGroupByIdAsync(assignmentGroupId, requesterId);
    var course = await this._context.Courses.FindAsync(assignmentGroup.CourseId);
    if (course is null) ServiceException.NotFound("Course not found.", course);

    assignmentGroup.Name = request.Name;
    assignmentGroup.Description = request.Description;
    assignmentGroup.Weight = request.Weight!.Value;
    course.UpdatedOnUtc = DateTime.UtcNow;

    await this._context.SaveChangesAsync();
    return await this.GetAssignmentGroupByIdAsync(assignmentGroupId, requesterId);
  }

  public async Task DeleteAssignmentGroupAsync(long assignmentGroupId, long requesterId)
  {
    var assignmentGroup = await this.GetAssignmentGroupByIdAsync(assignmentGroupId, requesterId);
    var course = await this._context.Courses.FindAsync(assignmentGroup.CourseId);
    if (course is null) ServiceException.NotFound("Course not found.", course);

    this._context.AssignmentGroups.Remove(assignmentGroup);
    foreach (var assignment in assignmentGroup.Assignments.ToList())
      await this._assignmentService.DeleteAssignmentAsync(assignment.Id, requesterId);
    course.UpdatedOnUtc = DateTime.UtcNow;

    await this._context.SaveChangesAsync();
  }
}
