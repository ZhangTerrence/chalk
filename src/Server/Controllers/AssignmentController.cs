using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Common.DTOs;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.Assignment;
using Server.Common.Responses;
using Server.Common.Utilities;

namespace Server.Controllers;

[ApiController]
[Authorize]
[Route("/api/assignments")]
public class AssignmentController : ControllerBase
{
  private readonly IAssignmentService _assignmentService;

  public AssignmentController(IAssignmentService assignmentService)
  {
    this._assignmentService = assignmentService;
  }

  [HttpPost]
  public async Task<IActionResult> CreateAssignment([FromBody] CreateRequest request)
  {
    var assignment = await this._assignmentService.CreateAssignmentAsync(this.User.GetUserId(), request);
    return this.Created(nameof(this.CreateAssignment), new Response<AssignmentDto>(null, assignment.ToDto()));
  }

  [HttpPut("{assignmentId:long}")]
  public async Task<IActionResult> UpdateAssignment([FromRoute] long assignmentId, [FromBody] UpdateRequest request)
  {
    var assignment = await this._assignmentService.UpdateAssignmentAsync(assignmentId, this.User.GetUserId(), request);
    return this.Ok(new Response<AssignmentDto>(null, assignment.ToDto()));
  }

  [HttpDelete("{assignmentId:long}")]
  public async Task<IActionResult> DeleteAssignment([FromRoute] long assignmentId)
  {
    await this._assignmentService.DeleteAssignmentAsync(assignmentId, this.User.GetUserId());
    return this.NoContent();
  }
}
