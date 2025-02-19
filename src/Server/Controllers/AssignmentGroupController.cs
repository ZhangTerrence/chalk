using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
  /// Gets a course's assignment groups.
  /// </summary>
  /// <param name="courseId">The course's id.</param>
  /// <returns>A list of all the course's assignment groups.</returns>
  [HttpGet("course/{courseId:long}")]
  [ProducesResponseType<Response<IEnumerable<AssignmentGroupResponse>>>(StatusCodes.Status200OK)]
  public async Task<IActionResult> GetCourseAssignmentGroups([FromRoute] long courseId)
  {
    var assignmentGroups =
      await this._assignmentGroupService.GetCourseAssignmentGroupsAsync(courseId, this.User.GetUserId());
    return this.Ok(
      new Response<IEnumerable<AssignmentGroupResponse>>(null, assignmentGroups.Select(e => e.ToResponse())));
  }

  /// <summary>
  /// Creates an assignment group.
  /// </summary>
  /// <param name="request">The request body. See <see cref="CreateRequest" /> for more details.</param>
  /// <returns>The created assignment group.</returns>
  [HttpPost]
  [ProducesResponseType<Response<AssignmentGroupResponse>>(StatusCodes.Status201Created)]
  public async Task<IActionResult> CreateAssignmentGroup([FromBody] CreateRequest request)
  {
    var assignmentGroup = await this._assignmentGroupService.CreateAssignmentGroupAsync(this.User.GetUserId(), request);
    return this.Created(nameof(this.CreateAssignmentGroup),
      new Response<AssignmentGroupResponse>(null, assignmentGroup.ToResponse()));
  }

  /// <summary>
  /// Updates an assignment group.
  /// </summary>
  /// <param name="assignmentGroupId">The assignment group's id.</param>
  /// <param name="request">The request body. See <see cref="UpdateRequest" /> for more details.</param>
  /// <returns>The updated assignment group.</returns>
  [HttpPut("{assignmentGroupId:long}")]
  [ProducesResponseType<Response<AssignmentGroupResponse>>(StatusCodes.Status200OK)]
  public async Task<IActionResult> UpdateAssignmentGroup([FromRoute] long assignmentGroupId,
    [FromBody] UpdateRequest request)
  {
    var assignmentGroup =
      await this._assignmentGroupService.UpdateAssignmentGroupAsync(assignmentGroupId, this.User.GetUserId(), request);
    return this.Ok(new Response<AssignmentGroupResponse>(null, assignmentGroup.ToResponse()));
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
