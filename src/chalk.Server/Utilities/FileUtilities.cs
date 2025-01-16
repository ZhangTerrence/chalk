using System.Security.Cryptography;
using System.Text;

namespace chalk.Server.Utilities;

public static class FileUtilities
{
    public static string S3ObjectHash(string resourceType, string uid)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes($"{resourceType}:{uid}"));

        var builder = new StringBuilder();
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }

    public static bool IsImage(this string contentType)
    {
        switch (contentType)
        {
            case "image/jpeg":
            case "image/png":
                return true;
            default:
                return false;
        }
    }
}