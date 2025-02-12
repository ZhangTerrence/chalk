using Server.Common.Requests.AssignmentGroup;
using Server.Data.Entities;

namespace Server.Common.Interfaces;

public interface IAssignmentGroupService
{
  public Task<AssignmentGroup> GetAssignmentGroupByIdAsync(long assignmentGroupId, long requesterId);

  public Task<AssignmentGroup> CreateAssignmentGroupAsync(long requesterId, CreateRequest request);

  public Task<AssignmentGroup> UpdateAssignmentGroupAsync(long assignmentGroupId, long requesterId,
    UpdateRequest request);

  public Task DeleteAssignmentGroupAsync(long assignmentGroupId, long requesterId);
}
