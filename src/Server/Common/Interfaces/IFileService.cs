using Server.Common.Requests.File;
using File = Server.Data.Entities.File;

namespace Server.Common.Interfaces;

public interface IFileService
{
  public Task<File> GetFileByIdAsync(int fileId, long requesterId);

  public Task<T?> CreateFile<T>(long requesterId, CreateRequest request);

  public Task<T?> UpdateFile<T>(long fileId, long requesterId, UpdateRequest request);

  public Task DeleteFile(long fileId, long requesterId);
}
