using Server.Common.Requests.Module;
using Server.Data.Entities;

namespace Server.Common.Interfaces;

public interface IModuleService
{
  public Task<Module> GetModuleByIdAsync(long moduleId, long requesterId);

  public Task<Module> CreateModuleAsync(long requesterId, CreateRequest request);

  public Task<Course> ReorderModulesAsync(long courseId, long requesterId, ReorderRequest request);

  public Task<Module> UpdateModuleAsync(long moduleId, long requesterId, UpdateRequest request);

  public Task DeleteModuleAsync(long moduleId, long requesterId);
}
