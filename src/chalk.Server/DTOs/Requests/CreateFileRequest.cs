namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to create a file for either a module, assignment, or submission.
/// </summary>
/// <param name="Name">The file's name.</param>
/// <param name="Description">The file's description.</param>
/// <param name="File">The file.</param>
/// <remarks><paramref name="Name"/> is required.</remarks>
public record CreateFileRequest(
    string Name,
    string? Description,
    IFormFile File
);