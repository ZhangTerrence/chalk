namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to create an assignment. 
/// </summary>
/// <param name="Name">The assignment's name.</param>
/// <param name="Description">The assignment's description.</param>
/// <param name="IsOpen">Whether the assignment is open for submissions.</param>
/// <param name="DueDate">The assignment's due date.</param>
/// <param name="AllowedAttempts">The assignment's maximum allowed attempts.</param>
/// <remarks><paramref name="Name"/> and <paramref name="IsOpen"/> are required.</remarks>
public record CreateAssignmentRequest(
    string Name,
    string? Description,
    bool? IsOpen,
    DateTime? DueDate,
    int? AllowedAttempts
);