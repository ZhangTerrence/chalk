using chalk.Server.DTOs.Requests;
using chalk.Server.Entities;
using File = chalk.Server.Entities.File;

namespace chalk.Server.Services.Interfaces;

public interface IFileService
{
    public Task<Module> CreateFileForModule(long moduleId, CreateFileRequest request);

    public Task<File> UpdateFile(long fileId, UpdateFileRequest request);

    public Task DeleteFile(long fileId);
}