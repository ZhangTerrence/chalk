using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Transfer;
using chalk.Server.Configurations;
using chalk.Server.Services.Interfaces;
using chalk.Server.Utilities;

namespace chalk.Server.Services;

public class FileUploadService : IFileUploadService
{
    private const string BucketName = "chalk-s3";
    private const string Location = "us-east-1";

    private readonly AmazonS3Client _s3Client;

    public FileUploadService(IConfiguration configuration)
    {
        var chain = new CredentialProfileStoreChain();
        if (!chain.TryGetAWSCredentials(configuration["AWS_SDK:Profile"]!, out var credentials))
        {
            throw new ServiceException("AWS credentials not found", StatusCodes.Status500InternalServerError);
        }

        Console.WriteLine(credentials.GetCredentials().AccountId);

        _s3Client = new AmazonS3Client(credentials);
    }

    public async Task<string> UploadAsync(string hash, IFormFile file)
    {
        if (file.Length > 1e+7) // 10 MB
        {
            throw new ServiceException("File is too large.", StatusCodes.Status403Forbidden);
        }

        var isImage = file.ContentType.IsImage();
        if (!isImage)
        {
            throw new ServiceException("File is not an accepted image.", StatusCodes.Status400BadRequest);
        }

        var fileStream = new MemoryStream();
        await file.CopyToAsync(fileStream);

        var request = new TransferUtilityUploadRequest
        {
            InputStream = fileStream,
            BucketName = BucketName,
            Key = hash,
            CannedACL = S3CannedACL.PublicRead,
            ContentType = file.ContentType,
        };

        var fileTransferUtility = new TransferUtility(_s3Client);
        await fileTransferUtility.UploadAsync(request);

        return $"https://s3.{Location}.amazonaws.com/{BucketName}/{hash}";
    }
}