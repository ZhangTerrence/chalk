namespace Server.Common.Requests.Assignment;

/// <summary>
/// A request to create an assignment.
/// </summary>
/// <param name="AssignmentGroupId">The id of the assignment group the assignment belongs to. Required. </param>
/// <param name="Name">The assignment's name. Required.</param>
/// <param name="Description">The assignment's description.</param>
/// <param name="DueOnUtc">The assignment's due date.</param>
public record CreateRequest(
  long? AssignmentGroupId,
  string Name,
  string? Description,
  DateTime? DueOnUtc
);
