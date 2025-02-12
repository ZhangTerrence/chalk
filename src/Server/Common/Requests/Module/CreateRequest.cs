namespace Server.Common.Requests.Module;

/// <summary>
/// A request to create a module.
/// </summary>
/// <param name="CourseId">The id of the course the module belongs to. Required. </param>
/// <param name="Name">The module's name. Required.</param>
/// <param name="Description">The module's description.</param>
public record CreateRequest(
  long? CourseId,
  string Name,
  string? Description
);
