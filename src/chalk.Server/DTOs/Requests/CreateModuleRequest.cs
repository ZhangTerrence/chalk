namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to create a module.
/// </summary>
/// <param name="Name">The module's name.</param>
/// <param name="Description">The module's description.</param>
/// <remarks><paramref name="Name"/> is required.</remarks>
public record CreateModuleRequest(
    string Name,
    string? Description
);