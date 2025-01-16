namespace chalk.Server.Services.Interfaces;

public interface IFileUploadService
{
    public Task<string> UploadAsync(string hash, IFormFile file);
}