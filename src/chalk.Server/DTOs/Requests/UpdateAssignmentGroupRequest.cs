namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to create an assignment group.
/// </summary>
/// <param name="Name">The assignment group's name.</param>
/// <param name="Description">The assignment group's description.</param>
/// <param name="Weight">The assignment group's weight.</param>
/// <remarks><paramref name="Name"/> and <paramref name="Weight"/> are required.</remarks>
public record UpdateAssignmentGroupRequest(
    string Name,
    string? Description,
    int? Weight
);