using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
  /// Gets a course's modules.
  /// </summary>
  /// <param name="courseId">The course's id.</param>
  /// <returns>A list of all the course's modules.</returns>
  [HttpGet("course/{courseId:long}")]
  [ProducesResponseType<Response<IEnumerable<ModuleResponse>>>(StatusCodes.Status200OK)]
  public async Task<IActionResult> GetCourseModules([FromRoute] long courseId)
  {
    var modules = await this._moduleService.GetCourseModulesAsync(courseId, this.User.GetUserId());
    return this.Ok(new Response<IEnumerable<ModuleResponse>>(null, modules.Select(e => e.ToResponse())));
  }

  /// <summary>
  /// Creates a module.
  /// </summary>
  /// <param name="request">The request body. See <see cref="CreateRequest" /> for more details.</param>
  /// <returns>The created module.</returns>
  [HttpPost]
  [ProducesResponseType<Response<ModuleResponse>>(StatusCodes.Status201Created)]
  public async Task<IActionResult> CreateModule([FromBody] CreateRequest request)
  {
    var module = await this._moduleService.CreateModuleAsync(this.User.GetUserId(), request);
    return this.Created(nameof(this.CreateModule), new Response<ModuleResponse>(null, module.ToResponse()));
  }

  /// <summary>
  /// Reorders the modules in a course.
  /// </summary>
  /// <param name="request">The request body. See <see cref="ReorderModules" /> for more details.</param>
  /// <returns>The updated course.</returns>
  [HttpPatch("reorder")]
  [ProducesResponseType<Response<CourseResponse>>(StatusCodes.Status200OK)]
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
  [ProducesResponseType<Response<ModuleResponse>>(StatusCodes.Status200OK)]
  public async Task<IActionResult> UpdateCourseModule([FromRoute] long moduleId, [FromBody] UpdateRequest request)
  {
    var module = await this._moduleService.UpdateModuleAsync(moduleId, this.User.GetUserId(), request);
    return this.Ok(new Response<ModuleResponse>(null, module.ToResponse()));
  }

  /// <summary>
  /// Deletes a module.
  /// </summary>
  /// <param name="moduleId">The module's id.</param>
  [HttpDelete("{moduleId:long}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  public async Task<IActionResult> DeleteCourseModule([FromRoute] long moduleId)
  {
    await this._moduleService.DeleteModuleAsync(moduleId, this.User.GetUserId());
    return this.NoContent();
  }
}
