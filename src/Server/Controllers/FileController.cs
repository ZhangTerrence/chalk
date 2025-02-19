using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Common.Enums;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.File;
using Server.Common.Responses;
using Server.Common.Utilities;
using Server.Data.Entities;
using Server.Infrastructure.Filters;

namespace Server.Controllers;

/// <summary>
/// Routes for managing files.
/// </summary>
[ApiController] [Authorize]
[Route("/api/files")]
[Produces("application/json")]
public class FileController : ControllerBase
{
  private readonly IFileService _fileService;

  internal FileController(IFileService fileService)
  {
    this._fileService = fileService;
  }

  /// <summary>
  /// Creates a file.
  /// </summary>
  /// <param name="request">The request body. See <see cref="CreateRequest" /> for more details.</param>
  /// <returns>The container of the created file.</returns>
  [HttpPost]
  [ProducesResponseType<Response<FileContainerResponse>>(StatusCodes.Status201Created)]
  public async Task<IActionResult> CreateFile([FromForm] [Validate] CreateRequest request)
  {
    switch (request.For)
    {
      case FileFor.Module:
        var module = await this._fileService.CreateFile<Module>(this.User.GetUserId(), request);
        return this.Ok(module?.ToResponse());
      case FileFor.Assignment:
        var assignment = await this._fileService.CreateFile<Assignment>(this.User.GetUserId(), request);
        return this.Ok(assignment?.ToResponse());
      case FileFor.Submission:
        return this.NoContent();
      default:
        return this.BadRequest(new Response<object>(new Dictionary<string, string[]>
          { { "Bad Request.", ["Invalid choice."] } }));
    }
  }

  /// <summary>
  /// Updates a file.
  /// </summary>
  /// <param name="fileId">The file's id.</param>
  /// <param name="request">The request body. See <see cref="UpdateRequest" /> for more details.</param>
  /// <returns>The container of the updated file.</returns>
  [HttpPut("{fileId:long}")]
  [ProducesResponseType<Response<FileContainerResponse>>(StatusCodes.Status200OK)]
  public async Task<IActionResult> UpdateFile([FromRoute] long fileId, [FromForm] [Validate] UpdateRequest request)
  {
    switch (request.For)
    {
      case FileFor.Module:
        var module = await this._fileService.UpdateFile<Module>(fileId, this.User.GetUserId(), request);
        return this.Ok(new Response<ModuleResponse>(null, module?.ToResponse()));
      case FileFor.Assignment:
        var assignment = await this._fileService.UpdateFile<Assignment>(fileId, this.User.GetUserId(), request);
        return this.Ok(new Response<AssignmentResponse>(null, assignment?.ToResponse()));
      case FileFor.Submission:
        return this.NoContent();
      default:
        return this.BadRequest(new Response<object>(new Dictionary<string, string[]>
          { { "Bad Request.", ["Invalid choice."] } }));
    }
  }

  /// <summary>
  /// Deletes a file.
  /// </summary>
  /// <param name="fileId">The file's id.</param>
  [HttpDelete("{fileId:long}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  public async Task<IActionResult> DeleteModuleFile([FromRoute] long fileId)
  {
    await this._fileService.DeleteFile(fileId, this.User.GetUserId());
    return this.NoContent();
  }
}
