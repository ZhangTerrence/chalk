using Microsoft.EntityFrameworkCore;
using Server.Common.Enums;
using Server.Common.Exceptions;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.File;
using Server.Data;
using File = Server.Data.Entities.File;

namespace Server.Services;

internal class FileService : IFileService
{
  private readonly ICloudService _cloudService;
  private readonly DatabaseContext _context;

  public FileService(DatabaseContext context, ICloudService cloudService)
  {
    this._context = context;
    this._cloudService = cloudService;
  }

  public async Task<File> GetFileByIdAsync(int fileId, long requesterId)
  {
    var file = await this._context.Files.FindAsync(fileId);
    if (file is null) ServiceException.NotFound("File not found.", file);

    return file;
  }

  public async Task<T?> CreateFile<T>(long requesterId, CreateRequest request)
  {
    switch (request.For)
    {
      case FileFor.Module:
      {
        var module = await this._context.Modules
          .Include(e => e.Files)
          .FirstOrDefaultAsync(e => e.Id == request.ContainerId);
        if (module is null) ServiceException.NotFound("Module not found.", module);
        var course = await this._context.Courses.FindAsync(module.CourseId);
        if (course is null) ServiceException.NotFound("Course not found.", course);

        var url = await this._cloudService.UploadAsync(Guid.NewGuid().ToString(), request.File);
        var file = request.ToEntity(url);
        module.Files.Add(file);
        module.UpdatedOnUtc = DateTime.UtcNow;
        course.UpdatedOnUtc = DateTime.UtcNow;

        await this._context.SaveChangesAsync();
        return (T)Convert.ChangeType(module, typeof(T));
      }
      case FileFor.Assignment:
      {
        var assignment = this._context.Assignments
          .Include(e => e.Files)
          .FirstOrDefault(e => e.Id == request.ContainerId);
        if (assignment is null) ServiceException.NotFound("Assignment not found.", assignment);
        var assignmentGroup = await this._context.AssignmentGroups.FindAsync(assignment.AssignmentGroupId);
        if (assignmentGroup is null) ServiceException.NotFound("Assignment group not found.", assignmentGroup);
        var course = await this._context.Courses.FindAsync(assignmentGroup.CourseId);
        if (course is null) ServiceException.NotFound("Course not found.", course);

        var url = await this._cloudService.UploadAsync(Guid.NewGuid().ToString(), request.File);
        var file = request.ToEntity(url);
        assignment.Files.Add(file);
        assignment.UpdatedOnUtc = DateTime.UtcNow;
        assignmentGroup.UpdatedOnUtc = DateTime.UtcNow;
        course.UpdatedOnUtc = DateTime.UtcNow;

        await this._context.SaveChangesAsync();
        return (T)Convert.ChangeType(assignment, typeof(T));
      }
      case FileFor.Submission:
        return default;
      default:
        ServiceException.BadRequest("Invalid choice.");
        return default;
    }
  }

  public async Task<T?> UpdateFile<T>(long fileId, long requesterId, UpdateRequest request)
  {
    switch (request.For)
    {
      case FileFor.Module:
      {
        var module = await this._context.Modules
          .Include(e => e.Files)
          .FirstOrDefaultAsync(e => e.Id == request.ContainerId);
        if (module is null) ServiceException.NotFound("Module not found.", module);
        var course = await this._context.Courses.FindAsync(module.CourseId);
        if (course is null) ServiceException.NotFound("Course not found.", course);
        var file = await this._context.Files.FindAsync(fileId);
        if (file is null) ServiceException.NotFound("File not found.", file);

        file.Name = request.Name;
        file.Description = request.Description;
        if (request.FileChanged!.Value)
        {
          await this._cloudService.DeleteAsync(file.FileUrl);
          file.FileUrl = await this._cloudService.UploadAsync(Guid.NewGuid().ToString(), request.NewFile!);
        }
        module.UpdatedOnUtc = DateTime.UtcNow;
        course.UpdatedOnUtc = DateTime.UtcNow;
        file.UpdatedOnUtc = DateTime.UtcNow;

        await this._context.SaveChangesAsync();
        return (T)Convert.ChangeType(module, typeof(T));
      }
      case FileFor.Assignment:
      {
        var assignment = this._context.Assignments
          .Include(e => e.Files)
          .FirstOrDefault(e => e.Id == request.ContainerId);
        if (assignment is null) ServiceException.NotFound("Assignment not found.", assignment);
        var assignmentGroup = await this._context.AssignmentGroups.FindAsync(assignment.AssignmentGroupId);
        if (assignmentGroup is null) ServiceException.NotFound("Assignment group not found.", assignmentGroup);
        var course = await this._context.Courses.FindAsync(assignmentGroup.CourseId);
        if (course is null) ServiceException.NotFound("Course not found.", course);
        var file = await this._context.Files.FindAsync(fileId);
        if (file is null) ServiceException.NotFound("File not found.", file);

        file.Name = request.Name;
        file.Description = request.Description;
        if (request.FileChanged!.Value)
        {
          await this._cloudService.DeleteAsync(file.FileUrl);
          file.FileUrl = await this._cloudService.UploadAsync(Guid.NewGuid().ToString(), request.NewFile!);
        }
        assignment.UpdatedOnUtc = DateTime.UtcNow;
        assignmentGroup.UpdatedOnUtc = DateTime.UtcNow;
        course.UpdatedOnUtc = DateTime.UtcNow;
        file.UpdatedOnUtc = DateTime.UtcNow;

        await this._context.SaveChangesAsync();
        return (T)Convert.ChangeType(assignment, typeof(T));
      }
      case FileFor.Submission:
        return default;
      default:
        ServiceException.BadRequest("Invalid choice.");
        return default;
    }
  }

  public async Task DeleteFile(long fileId, long requesterId)
  {
    var file = await this._context.Files.FindAsync(fileId);
    if (file is null) ServiceException.NotFound("File not found.", file);

    await this._cloudService.DeleteAsync(file.FileUrl);
    this._context.Files.Remove(file);

    await this._context.SaveChangesAsync();
  }
}
