using Server.Common.Enums;

namespace Server.Common.Requests.File;

/// <summary>
/// A request to update a file.
/// </summary>
/// <param name="For">Whether the file is for either a module, assignment, or submission. Required.</param>
/// <param name="EntityId">The id of the module, assignment, or submission. Required.</param>
/// <param name="Name">The file's name. Required.</param>
/// <param name="Description">The file's description.</param>
/// <param name="FileChanged">Whether the file has changed. Required.</param>
/// <param name="NewFile">The new file. Required if <paramref name="FileChanged" /> is true.</param>
public record UpdateRequest(
  FileFor? For,
  long? EntityId,
  string Name,
  string? Description,
  bool? FileChanged,
  IFormFile? NewFile
);