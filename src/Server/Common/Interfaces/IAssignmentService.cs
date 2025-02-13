using Server.Common.Requests.Assignment;
using Server.Data.Entities;

namespace Server.Common.Interfaces;

/// <summary>
/// Interface for assignment services.
/// </summary>
public interface IAssignmentService
{
  /// <summary>
  /// Gets an assignment by id.
  /// </summary>
  /// <param name="assignmentId">The assignment's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  /// <returns>The assignment.</returns>
  public Task<Assignment> GetAssignmentByIdAsync(long assignmentId, long requesterId);

  /// <summary>
  /// Creates an assignment.
  /// </summary>
  /// <param name="requesterId">The requester's id.</param>
  /// <param name="request">The request body. See <see cref="CreateRequest" /> for more details.</param>
  /// <returns>The created assignment.</returns>
  public Task<Assignment> CreateAssignmentAsync(long requesterId, CreateRequest request);

  /// <summary>
  /// Updates an assignment.
  /// </summary>
  /// <param name="assignmentId">The assignment's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  /// <param name="request">The request body. See <see cref="UpdateRequest" /> for more details.</param>
  /// <returns>The updated assignment.</returns>
  public Task<Assignment> UpdateAssignmentAsync(long assignmentId, long requesterId, UpdateRequest request);

  /// <summary>
  /// Deletes an assignment.
  /// </summary>
  /// <param name="assignmentId">The assignment's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  public Task DeleteAssignmentAsync(long assignmentId, long requesterId);
}
