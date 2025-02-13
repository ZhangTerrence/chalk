using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Common.DTOs;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.Module;
using Server.Common.Responses;
using Server.Common.Utilities;

namespace Server.Controllers;

/// <summary>
/// Routes for managing modules.
/// </summary>
[ApiController] [Authorize]
[Route("/api/modules")]
[Produces("application/json")]
public class ModuleController : ControllerBase
{
  private readonly IModuleService _moduleService;

  internal ModuleController(IModuleService moduleService)
  {
    this._moduleService = moduleService;
  }

  /// <summary>
  /// Creates a module.
  /// </summary>
  /// <param name="request">The request body. See <see cref="CreateRequest" /> for more details.</param>
  /// <returns>The created module.</returns>
  [HttpPost]
  public async Task<IActionResult> CreateModule([FromBody] CreateRequest request)
  {
    var module = await this._moduleService.CreateModuleAsync(this.User.GetUserId(), request);
    return this.Created(nameof(this.CreateModule), new Response<ModuleDto>(null, module.ToDto()));
  }

  /// <summary>
  /// Reorders the modules in a course.
  /// </summary>
  /// <param name="request">The request body. See <see cref="ReorderModules" /> for more details.</param>
  /// <returns>The updated course.</returns>
  [HttpPatch("reorder")]
  public async Task<IActionResult> ReorderModules([FromBody] ReorderRequest request)
  {
    var course = await this._moduleService.ReorderModulesAsync(this.User.GetUserId(), request);
    return this.Ok(new Response<CourseResponse>(null, course.ToResponse()));
  }

  /// <summary>
  /// Updates a module.
  /// </summary>
  /// <param name="moduleId">The module's id.</param>
  /// <param name="request">The request body. See <see cref="UpdateRequest" /> for more details.</param>
  /// <returns>The updated module.</returns>
  [HttpPut("{moduleId:long}")]
  public async Task<IActionResult> UpdateCourseModule([FromRoute] long moduleId, [FromBody] UpdateRequest request)
  {
    var module = await this._moduleService.UpdateModuleAsync(moduleId, this.User.GetUserId(), request);
    return this.Ok(new Response<ModuleDto>(null, module.ToDto()));
  }

  /// <summary>
  /// Deletes a module.
  /// </summary>
  /// <param name="moduleId">The module's id.</param>
  [HttpDelete("{moduleId:long}")]
  public async Task<IActionResult> DeleteCourseModule([FromRoute] long moduleId)
  {
    await this._moduleService.DeleteModuleAsync(moduleId, this.User.GetUserId());
    return this.NoContent();
  }
}
