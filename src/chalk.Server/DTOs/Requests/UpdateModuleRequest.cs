namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to update a module.
/// </summary>
/// <param name="Name">The module's name.</param>
/// <param name="Description">The module's description.</param>
/// <remarks><paramref name="Name"/> is required.</remarks>
public record UpdateModuleRequest(
    string Name,
    string? Description
);