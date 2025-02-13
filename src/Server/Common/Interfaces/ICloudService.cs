namespace Server.Common.Interfaces;

/// <summary>
/// Interface for cloud services.
/// </summary>
public interface ICloudService
{
  /// <summary>
  /// Uploads a file to S3.
  /// </summary>
  /// <param name="hash">The file's hash.</param>
  /// <param name="file">The file.</param>
  /// <returns>The file's public S3 url.</returns>
  public Task<string> UploadAsync(string hash, IFormFile file);

  /// <summary>
  /// Uploads an image to S3.
  /// </summary>
  /// <param name="hash">The image's hash.</param>
  /// <param name="file">The image.</param>
  /// <returns>The image's public S3 url.</returns>
  public Task<string> UploadImageAsync(string hash, IFormFile file);

  /// <summary>
  /// Deletes a file from S3.
  /// </summary>
  /// <param name="url">The file's public S3 url.</param>
  public Task DeleteAsync(string url);
}
