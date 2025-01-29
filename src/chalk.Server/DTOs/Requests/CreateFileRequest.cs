using chalk.Server.Shared;

namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to create a file for either a module, assignment, or submission.
/// </summary>
/// <param name="For">Whether the file is for either a module, assignment, or submission.</param>
/// <param name="EntityId">The id of the module, assignment, or submission.</param>
/// <param name="Name">The file's name.</param>
/// <param name="Description">The file's description.</param>
/// <param name="File">The file.</param>
/// <remarks><paramref name="For"/>, <paramref name="EntityId"/>, and <paramref name="Name"/> are required.</remarks>
public record CreateFileRequest(
    For? For,
    long? EntityId,
    string Name,
    string? Description,
    IFormFile File
);