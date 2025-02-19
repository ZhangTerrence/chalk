using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.Assignment;
using Server.Common.Responses;
using Server.Common.Utilities;

namespace Server.Controllers;

/// <summary>
/// Routes for managing assignments.
/// </summary>
[ApiController] [Authorize]
[Route("/api/assignments")]
[Produces("application/json")]
public class AssignmentController : ControllerBase
{
  private readonly IAssignmentService _assignmentService;

  internal AssignmentController(IAssignmentService assignmentService)
  {
    this._assignmentService = assignmentService;
  }

  /// <summary>
  /// Creates an assignment.
  /// </summary>
  /// <param name="request">The request body. See <see cref="CreateRequest" /> for more details.</param>
  /// <returns>The created assignment.</returns>
  [HttpPost]
  [ProducesResponseType<Response<AssignmentResponse>>(StatusCodes.Status201Created)]
  public async Task<IActionResult> CreateAssignment([FromBody] CreateRequest request)
  {
    var assignment = await this._assignmentService.CreateAssignmentAsync(this.User.GetUserId(), request);
    return this.Created(nameof(this.CreateAssignment), new Response<AssignmentResponse>(null, assignment.ToResponse()));
  }

  /// <summary>
  /// Updates an assignment.
  /// </summary>
  /// <param name="assignmentId">The assignment's id.</param>
  /// <param name="request">The request body. See <see cref="UpdateRequest" /> for more details.</param>
  /// <returns>The updated assignment.</returns>
  [HttpPut("{assignmentId:long}")]
  [ProducesResponseType<Response<AssignmentResponse>>(StatusCodes.Status200OK)]
  public async Task<IActionResult> UpdateAssignment([FromRoute] long assignmentId, [FromBody] UpdateRequest request)
  {
    var assignment = await this._assignmentService.UpdateAssignmentAsync(assignmentId, this.User.GetUserId(), request);
    return this.Ok(new Response<AssignmentResponse>(null, assignment.ToResponse()));
  }

  /// <summary>
  /// Deletes an assignment.
  /// </summary>
  /// <param name="assignmentId">The assignment's id.</param>
  [HttpDelete("{assignmentId:long}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  public async Task<IActionResult> DeleteAssignment([FromRoute] long assignmentId)
  {
    await this._assignmentService.DeleteAssignmentAsync(assignmentId, this.User.GetUserId());
    return this.NoContent();
  }
}
