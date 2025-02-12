using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Common.DTOs;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.Module;
using Server.Common.Responses;
using Server.Common.Utilities;

namespace Server.Controllers;

[ApiController]
[Authorize]
[Route("/api/modules")]
public class ModuleController : ControllerBase
{
  private readonly IModuleService _moduleService;

  public ModuleController(IModuleService moduleService)
  {
    this._moduleService = moduleService;
  }

  [HttpPost]
  public async Task<IActionResult> CreateModule([FromBody] CreateRequest request)
  {
    var module = await this._moduleService.CreateModuleAsync(this.User.GetUserId(), request);
    return this.Created(nameof(this.CreateModule), new Response<ModuleDto>(null, module.ToDto()));
  }

  [HttpPut("reorder/{courseId:long}")]
  public async Task<IActionResult> ReorderModules([FromRoute] long courseId, [FromBody] ReorderRequest request)
  {
    var course = await this._moduleService.ReorderModulesAsync(courseId, this.User.GetUserId(), request);
    return this.Ok(new Response<CourseResponse>(null, course.ToResponse()));
  }

  [HttpPut("{moduleId:long}")]
  public async Task<IActionResult> UpdateCourseModule([FromRoute] long moduleId, [FromBody] UpdateRequest request)
  {
    var module = await this._moduleService.UpdateModuleAsync(moduleId, this.User.GetUserId(), request);
    return this.Ok(new Response<ModuleDto>(null, module.ToDto()));
  }

  [HttpDelete("{moduleId:long}")]
  public async Task<IActionResult> DeleteCourseModule([FromRoute] long moduleId)
  {
    await this._moduleService.DeleteModuleAsync(moduleId, this.User.GetUserId());
    return this.NoContent();
  }
}
