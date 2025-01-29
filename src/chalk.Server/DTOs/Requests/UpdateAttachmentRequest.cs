namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to update a file.
/// </summary>
/// <param name="Name">The file's name.</param>
/// <param name="Description">The file's description.</param>
/// <param name="UpdatedFile">Whether the file has been changed.</param>
/// <param name="File">The file.</param>
/// <remarks><paramref name="Name"/> and <paramref name="UpdatedFile"/> are required.</remarks>
public record UpdateFileRequest(
    string Name,
    string? Description,
    bool? UpdatedFile,
    IFormFile File
);