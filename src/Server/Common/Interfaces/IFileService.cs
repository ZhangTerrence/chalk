using Server.Common.Requests.File;
using File = Server.Data.Entities.File;

namespace Server.Common.Interfaces;

/// <summary>
/// Interface for file services.
/// </summary>
public interface IFileService
{
  /// <summary>
  /// Gets a file by id.
  /// </summary>
  /// <param name="fileId">The file's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  /// <returns>The file.</returns>
  public Task<File> GetFileByIdAsync(int fileId, long requesterId);

  /// <summary>
  /// Creates a file.
  /// </summary>
  /// <param name="requesterId">The requester's id.</param>
  /// <param name="request">The request body. See <see cref="CreateRequest" /> for more details.</param>
  /// <typeparam name="T">Whether it is for a module, assignment, or submission.</typeparam>
  /// <returns>The created file.</returns>
  public Task<T?> CreateFile<T>(long requesterId, CreateRequest request);

  /// <summary>
  /// Updates a file.
  /// </summary>
  /// <param name="fileId">The file's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  /// <param name="request">The request body. See <see cref="UpdateRequest" /> for more details.</param>
  /// <typeparam name="T">Whether it is for a module, assignment, or submission.</typeparam>
  /// <returns>The created file.</returns>
  public Task<T?> UpdateFile<T>(long fileId, long requesterId, UpdateRequest request);

  /// <summary>
  /// Deletes a file.
  /// </summary>
  /// <param name="fileId">The file's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  public Task DeleteFile(long fileId, long requesterId);
}
