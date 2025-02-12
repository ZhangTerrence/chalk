namespace Server.Common.Requests.Assignment;

/// <summary>
/// A request to update an assignment.
/// </summary>
/// <param name="Name">The assignment's name. Required.</param>
/// <param name="Description">The assignment's description.</param>
/// <param name="DueOnUtc">The assignment's due date.</param>
public record UpdateRequest(
  string Name,
  string? Description,
  DateTime? DueOnUtc
);
