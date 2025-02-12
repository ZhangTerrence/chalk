namespace Server.Common.Requests.Module;

/// <summary>
/// A request to update a module.
/// </summary>
/// <param name="Name">The module's name. Required.</param>
/// <param name="Description">The module's description.</param>
public record UpdateRequest(
  string Name,
  string? Description
);