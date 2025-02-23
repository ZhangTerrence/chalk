using Server.Common.Enums;

namespace Server.Common.Requests.File;

/// <summary>
/// A request to create a file for either a module, assignment, or submission.
/// </summary>
/// <param name="For">Whether the file is for either a module, assignment, or submission. Required.</param>
/// <param name="ContainerId">The id of the module, assignment, or submission. Required.</param>
/// <param name="Name">The file's name. Required.</param>
/// <param name="Description">The file's description.</param>
/// <param name="File">The file.</param>
public record CreateRequest(
  FileFor? For,
  long? ContainerId,
  string Name,
  string? Description,
  IFormFile File
);
