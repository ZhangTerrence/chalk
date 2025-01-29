namespace chalk.Server.Services.Interfaces;

public interface ICloudService
{
    public Task<string> UploadAsync(string hash, IFormFile file);

    public Task<string> UploadImageAsync(string hash, IFormFile file);

    public Task DeleteAsync(string hash);
}