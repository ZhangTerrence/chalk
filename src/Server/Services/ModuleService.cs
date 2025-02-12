using Microsoft.EntityFrameworkCore;
using Server.Common.Exceptions;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.Module;
using Server.Data;
using Server.Data.Entities;

namespace Server.Services;

public class ModuleService : IModuleService
{
  private readonly DatabaseContext _context;
  private readonly IFileService _fileService;

  public ModuleService(DatabaseContext context, IFileService fileService)
  {
    this._context = context;
    this._fileService = fileService;
  }

  public async Task<Module> GetModuleByIdAsync(long moduleId, long requesterId)
  {
    var module = await this._context.Modules
      .Include(e => e.Files).AsSingleQuery()
      .FirstOrDefaultAsync(e => e.Id == moduleId);
    if (module is null) ServiceException.NotFound("Module not found.", module);

    return module;
  }

  public async Task<Module> CreateModuleAsync(long requesterId, CreateRequest request)
  {
    var course = await this._context.Courses
      .Include(e => e.Modules)
      .FirstOrDefaultAsync(e => e.Id == request.CourseId!.Value);
    if (course is null) ServiceException.NotFound("Course not found.", course);
    var module = request.ToEntity(course.Modules.Select(e => e.RelativeOrder).DefaultIfEmpty(-1).Max() + 1);

    course.Modules.Add(module);
    course.UpdatedOnUtc = DateTime.UtcNow;

    await this._context.SaveChangesAsync();
    return await this.GetModuleByIdAsync(module.Id, requesterId);
  }

  public async Task<Course> ReorderModulesAsync(long courseId, long requesterId, ReorderRequest request)
  {
    var course = await this._context.Courses.FindAsync(courseId);
    if (course is null) ServiceException.NotFound("Course not found.", course);

    var i = 0;
    foreach (var moduleId in request.Modules)
    {
      var module = await this.GetModuleByIdAsync(moduleId, requesterId);
      module.RelativeOrder = i;
      i++;
    }
    course.UpdatedOnUtc = DateTime.UtcNow;

    await this._context.SaveChangesAsync();
    return course;
  }

  public async Task<Module> UpdateModuleAsync(long moduleId, long requesterId, UpdateRequest request)
  {
    var module = await this.GetModuleByIdAsync(moduleId, requesterId);
    var course = await this._context.Courses.FindAsync(module.CourseId);
    if (course is null) ServiceException.NotFound("Course not found.", course);

    module.Name = request.Name;
    module.Description = request.Description;
    module.UpdatedOnUtc = DateTime.UtcNow;
    course.UpdatedOnUtc = DateTime.UtcNow;

    await this._context.SaveChangesAsync();
    return await this.GetModuleByIdAsync(moduleId, requesterId);
  }

  public async Task DeleteModuleAsync(long moduleId, long requesterId)
  {
    var module = await this.GetModuleByIdAsync(moduleId, requesterId);
    var course = await this._context.Courses
      .Include(e => e.Modules)
      .FirstOrDefaultAsync(e => e.Id == module.CourseId);
    if (course is null) ServiceException.NotFound("Course not found.", course);

    this._context.Modules.Remove(module);
    foreach (var e in course.Modules.ToList().Where(e => e.RelativeOrder > module.RelativeOrder))
      e.RelativeOrder -= 1;
    foreach (var file in module.Files.ToList())
      await this._fileService.DeleteFile(file.Id, requesterId);
    course.UpdatedOnUtc = DateTime.UtcNow;

    await this._context.SaveChangesAsync();
  }
}
