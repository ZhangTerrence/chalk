using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Server.Common.Exceptions;
using Server.Common.Interfaces;
using Server.Common.Utilities;

namespace Server.Services;

public class CloudService : ICloudService
{
  private const string BucketName = "chalk-s3";
  private const string Location = "us-east-1";

  private readonly AmazonS3Client _s3Client;

  public CloudService(IConfiguration configuration)
  {
    var chain = new CredentialProfileStoreChain();
    if (!chain.TryGetAWSCredentials(configuration["AWS_SDK:Profile"]!, out var credentials))
      ServiceException.InternalServerError("Failed to retrieve AWS credentials.");
    this._s3Client = new AmazonS3Client(credentials);
  }

  public async Task<string> UploadAsync(string hash, IFormFile file)
  {
    if (file.Length > 1e+7) // 10 MB
      ServiceException.Forbidden("File too large.");
    var fileStream = new MemoryStream();
    await file.CopyToAsync(fileStream);
    var request = new TransferUtilityUploadRequest
    {
      InputStream = fileStream,
      BucketName = BucketName,
      Key = hash,
      CannedACL = S3CannedACL.PublicRead,
      ContentType = file.ContentType
    };
    var fileTransferUtility = new TransferUtility(this._s3Client);
    await fileTransferUtility.UploadAsync(request);
    return $"https://s3.{Location}.amazonaws.com/{BucketName}/{hash}";
  }

  public async Task<string> UploadImageAsync(string hash, IFormFile file)
  {
    var isImage = file.ContentType.IsAcceptedImage();
    if (!isImage) ServiceException.BadRequest("File is not an accepted image.");
    return await this.UploadAsync(hash, file);
  }

  public async Task DeleteAsync(string url)
  {
    var request = new DeleteObjectRequest
    {
      BucketName = BucketName,
      Key = url.Split("/").Last()
    };
    await this._s3Client.DeleteObjectAsync(request);
  }
}