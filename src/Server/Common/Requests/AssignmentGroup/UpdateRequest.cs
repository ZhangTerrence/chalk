namespace Server.Common.Requests.AssignmentGroup;

/// <summary>
/// A request to update an assignment group.
/// </summary>
/// <param name="Name">The assignment group's name. Required.</param>
/// <param name="Description">The assignment group's description.</param>
/// <param name="Weight">The assignment group's weight. Required.</param>
public record UpdateRequest(
  string Name,
  string? Description,
  int? Weight
);
