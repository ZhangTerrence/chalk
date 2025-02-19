namespace Server.Common.Requests.AssignmentGroup;

/// <summary>
/// A request to create an assignment group.
/// </summary>
/// <param name="CourseId">The id of the course the assignment group belongs to. Required.</param>
/// <param name="Name">The assignment group's name. Required.</param>
/// <param name="Description">The assignment group's description.</param>
/// <param name="Weight">The assignment group's weight. Required.</param>
public record CreateRequest(
  long? CourseId,
  string Name,
  string? Description,
  int? Weight
);
