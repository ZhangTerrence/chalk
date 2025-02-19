namespace Server.Common.Requests.Module;

/// <summary>
/// A request to reorder modules.
/// </summary>
/// <param name="CourseId">The id of the course the modules belong to. Required.</param>
/// <param name="Modules">An array of module ids sorted in the order to be reordered.</param>
public record ReorderRequest(
  long? CourseId,
  IEnumerable<long> Modules
);
