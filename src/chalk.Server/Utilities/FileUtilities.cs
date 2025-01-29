namespace chalk.Server.Utilities;

public static class FileUtilities
{
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