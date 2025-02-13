using Server.Common.Requests.AssignmentGroup;
using Server.Data.Entities;

namespace Server.Common.Interfaces;

/// <summary>
/// Interface for assignment group services.
/// </summary>
public interface IAssignmentGroupService
{
  /// <summary>
  /// Gets an assignment group by id.
  /// </summary>
  /// <param name="assignmentGroupId">The assignment group's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  /// <returns>The assignment group.</returns>
  public Task<AssignmentGroup> GetAssignmentGroupByIdAsync(long assignmentGroupId, long requesterId);

  /// <summary>
  /// Creates an assignment group.
  /// </summary>
  /// <param name="requesterId">The requester's id.</param>
  /// <param name="request">The request body. See <see cref="CreateRequest" /> for more details.</param>
  /// <returns>The created assignment group.</returns>
  public Task<AssignmentGroup> CreateAssignmentGroupAsync(long requesterId, CreateRequest request);

  /// <summary>
  /// Updates an assignment group.
  /// </summary>
  /// <param name="assignmentGroupId">The assignment group's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  /// <param name="request">The request body. See <see cref="UpdateRequest" /> for more details.</param>
  /// <returns>The updated assignment group.</returns>
  public Task<AssignmentGroup> UpdateAssignmentGroupAsync(long assignmentGroupId, long requesterId,
    UpdateRequest request);

  /// <summary>
  /// Deletes an assignment group.
  /// </summary>
  /// <param name="assignmentGroupId">The assignment group's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  public Task DeleteAssignmentGroupAsync(long assignmentGroupId, long requesterId);
}
