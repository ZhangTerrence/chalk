using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Common.DTOs;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.AssignmentGroup;
using Server.Common.Responses;
using Server.Common.Utilities;

namespace Server.Controllers;

[ApiController]
[Authorize]
[Route("/api/assignment-groups")]
public class AssignmentGroupController : ControllerBase
{
  private readonly IAssignmentGroupService _assignmentGroupService;

  public AssignmentGroupController(IAssignmentGroupService assignmentGroupService)
  {
    this._assignmentGroupService = assignmentGroupService;
  }

  [HttpPost]
  public async Task<IActionResult> CreateAssignmentGroup([FromBody] CreateRequest request)
  {
    var assignmentGroup = await this._assignmentGroupService.CreateAssignmentGroupAsync(this.User.GetUserId(), request);
    return this.Created(nameof(this.CreateAssignmentGroup),
      new Response<AssignmentGroupDto>(null, assignmentGroup.ToDto()));
  }

  [HttpPut("{assignmentGroupId:long}")]
  public async Task<IActionResult> UpdateAssignmentGroup([FromRoute] long assignmentGroupId,
    [FromBody] UpdateRequest request)
  {
    var assignmentGroup =
      await this._assignmentGroupService.UpdateAssignmentGroupAsync(assignmentGroupId, this.User.GetUserId(), request);
    return this.Ok(new Response<AssignmentGroupDto>(null, assignmentGroup.ToDto()));
  }

  [HttpDelete("{assignmentGroupId:long}")]
  public async Task<IActionResult> DeleteAssignmentGroup([FromRoute] long assignmentGroupId)
  {
    await this._assignmentGroupService.DeleteAssignmentGroupAsync(assignmentGroupId, this.User.GetUserId());
    return this.NoContent();
  }
}
