using chalk.Server.DTOs.Requests;

namespace chalk.Server.Services.Interfaces;

public interface IFileService
{
    public Task<T> CreateFile<T>(CreateFileRequest request);

    public Task<T> UpdateFile<T>(long fileId, UpdateFileRequest request);

    public Task DeleteFile(long fileId);
}