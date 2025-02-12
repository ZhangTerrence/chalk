using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Common.DTOs;
using Server.Common.Enums;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.File;
using Server.Common.Responses;
using Server.Common.Utilities;
using Server.Data.Entities;
using Server.Infrastructure.Filters;

namespace Server.Controllers;

[ApiController]
[Authorize]
[Route("/api/files")]
public class FileController : ControllerBase
{
  private readonly IFileService _fileService;

  public FileController(IFileService fileService)
  {
    this._fileService = fileService;
  }

  [HttpPost]
  public async Task<IActionResult> CreateFile([FromForm] [Validate] CreateRequest request)
  {
    switch (request.For)
    {
      case FileFor.Module:
        var module = await this._fileService.CreateFile<Module>(this.User.GetUserId(), request);
        return this.Ok(new Response<ModuleDto>(null, module.ToDto()));
      case FileFor.Assignment:
        var assignment = await this._fileService.CreateFile<Assignment>(this.User.GetUserId(), request);
        return this.Ok(new Response<AssignmentDto>(null, assignment.ToDto()));
      case FileFor.Submission:
        return this.NoContent();
      default:
        return this.BadRequest(new Response<object>(["Invalid choice."]));
    }
  }

  [HttpPut("{fileId:long}")]
  public async Task<IActionResult> UpdateFile([FromRoute] long fileId, [FromForm] [Validate] UpdateRequest request)
  {
    switch (request.For)
    {
      case FileFor.Module:
        var module = await this._fileService.UpdateFile<Module>(fileId, this.User.GetUserId(), request);
        return this.Ok(new Response<ModuleDto>(null, module.ToDto()));
      case FileFor.Assignment:
        var assignment = await this._fileService.UpdateFile<Assignment>(fileId, this.User.GetUserId(), request);
        return this.Ok(new Response<AssignmentDto>(null, assignment.ToDto()));
      case FileFor.Submission:
        return this.NoContent();
      default:
        return this.BadRequest(new Response<object>(["Invalid choice."]));
    }
  }

  [HttpDelete("{fileId:long}")]
  public async Task<IActionResult> DeleteModuleFile([FromRoute] long fileId)
  {
    await this._fileService.DeleteFile(fileId, this.User.GetUserId());
    return this.NoContent();
  }
}
