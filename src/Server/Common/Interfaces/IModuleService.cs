using Server.Common.Requests.Module;
using Server.Data.Entities;

namespace Server.Common.Interfaces;

/// <summary>
/// Interface for module services.
/// </summary>
public interface IModuleService
{
  /// <summary>
  /// Gets a course's modules.
  /// </summary>
  /// <param name="courseId">The course's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  /// <returns>A list of all the course's modules.</returns>
  public Task<IEnumerable<Module>> GetCourseModulesAsync(long courseId, long requesterId);

  /// <summary>
  /// Gets a module by id.
  /// </summary>
  /// <param name="moduleId">The module's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  /// <returns>The module.</returns>
  public Task<Module> GetModuleByIdAsync(long moduleId, long requesterId);

  /// <summary>
  /// Creates a module.
  /// </summary>
  /// <param name="requesterId">The requester's id.</param>
  /// <param name="request">The request body. See <see cref="CreateRequest" /> for more information.</param>
  /// <returns>The created module.</returns>
  public Task<Module> CreateModuleAsync(long requesterId, CreateRequest request);

  /// <summary>
  /// Reorders the modules in a course.
  /// </summary>
  /// <param name="requesterId">The requester's id.</param>
  /// <param name="request">The request body. See <see cref="ReorderRequest" /> for more details.</param>
  /// <returns>The updated course.</returns>
  public Task<Course> ReorderModulesAsync(long requesterId, ReorderRequest request);

  /// <summary>
  /// Updates a module.
  /// </summary>
  /// <param name="moduleId">The module's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  /// <param name="request">The request body. See <see cref="UpdateRequest" /> for more details.</param>
  /// <returns>The updated module.</returns>
  public Task<Module> UpdateModuleAsync(long moduleId, long requesterId, UpdateRequest request);

  /// <summary>
  /// Deletes a module.
  /// </summary>
  /// <param name="moduleId">The module's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  public Task DeleteModuleAsync(long moduleId, long requesterId);
}
