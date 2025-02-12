using Server.Common.Requests.Assignment;
using Server.Data.Entities;

namespace Server.Common.Interfaces;

public interface IAssignmentService
{
  public Task<Assignment> GetAssignmentByIdAsync(long assignmentId, long requesterId);

  public Task<Assignment> CreateAssignmentAsync(long requesterId, CreateRequest request);

  public Task<Assignment> UpdateAssignmentAsync(long assignmentId, long requesterId, UpdateRequest request);

  public Task DeleteAssignmentAsync(long assignmentId, long requesterId);
}
