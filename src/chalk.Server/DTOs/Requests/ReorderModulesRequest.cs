namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to reorder modules.
/// </summary>
/// <param name="Modules">An array of module ids sorted in the order to be reordered.</param>
public record ReorderModulesRequest(
    IEnumerable<long> Modules
);