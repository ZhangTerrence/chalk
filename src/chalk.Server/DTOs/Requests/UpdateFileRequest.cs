using chalk.Server.Shared;

namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to update a file.
/// </summary>
/// <param name="For">Whether the file is for either a module, assignment, or submission.</param>
/// <param name="EntityId">The id of the module, assignment, or submission.</param>
/// <param name="Name">The file's name.</param>
/// <param name="Description">The file's description.</param>
/// <param name="FileChanged">Whether the file has changed.</param>
/// <param name="NewFile">The new file.</param>
/// <remarks>
///     <paramref name="For"/>, <paramref name="EntityId"/>, <paramref name="Name"/>, and <paramref name="FileChanged"/> are required.
///     <paramref name="NewFile"/> is only required if <paramref name="FileChanged"/> is true.
/// </remarks>
public record UpdateFileRequest(
    For? For,
    long? EntityId,
    string Name,
    string? Description,
    bool? FileChanged,
    IFormFile? NewFile
);