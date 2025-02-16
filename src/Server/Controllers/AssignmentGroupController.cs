using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Common.DTOs;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.AssignmentGroup;
using Server.Common.Responses;
using Server.Common.Utilities;

namespace Server.Controllers;

/// <summary>
/// Routes for managing assignment groups.
/// </summary>
[ApiController] [Authorize]
[Route("/api/assignment-groups")]
[Produces("application/json")]
public class AssignmentGroupController : ControllerBase
{
  private readonly IAssignmentGroupService _assignmentGroupService;

  internal AssignmentGroupController(IAssignmentGroupService assignmentGroupService)
  {
    this._assignmentGroupService = assignmentGroupService;
  }

  /// <summary>
  /// Creates an assignment group.
  /// </summary>
  /// <param name="request">The request body. See <see cref="CreateRequest" /> for more details.</param>
  /// <returns>The created assignment group.</returns>
  [HttpPost]
  [ProducesResponseType<Response<AssignmentGroupDto>>(StatusCodes.Status201Created)]
  public async Task<IActionResult> CreateAssignmentGroup([FromBody] CreateRequest request)
  {
    var assignmentGroup = await this._assignmentGroupService.CreateAssignmentGroupAsync(this.User.GetUserId(), request);
    return this.Created(nameof(this.CreateAssignmentGroup),
      new Response<AssignmentGroupDto>(null, assignmentGroup.ToDto()));
  }

  /// <summary>
  /// Updates an assignment group.
  /// </summary>
  /// <param name="assignmentGroupId">The assignment group's id.</param>
  /// <param name="request">The request body. See <see cref="UpdateRequest" /> for more details.</param>
  /// <returns>The updated assignment group.</returns>
  [HttpPut("{assignmentGroupId:long}")]
  [ProducesResponseType<Response<AssignmentGroupDto>>(StatusCodes.Status200OK)]
  public async Task<IActionResult> UpdateAssignmentGroup([FromRoute] long assignmentGroupId,
    [FromBody] UpdateRequest request)
  {
    var assignmentGroup =
      await this._assignmentGroupService.UpdateAssignmentGroupAsync(assignmentGroupId, this.User.GetUserId(), request);
    return this.Ok(new Response<AssignmentGroupDto>(null, assignmentGroup.ToDto()));
  }

  /// <summary>
  /// Deletes an assignment group.
  /// </summary>
  /// <param name="assignmentGroupId">The assignment group's id.</param>
  [HttpDelete("{assignmentGroupId:long}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  public async Task<IActionResult> DeleteAssignmentGroup([FromRoute] long assignmentGroupId)
  {
    await this._assignmentGroupService.DeleteAssignmentGroupAsync(assignmentGroupId, this.User.GetUserId());
    return this.NoContent();
  }
}
