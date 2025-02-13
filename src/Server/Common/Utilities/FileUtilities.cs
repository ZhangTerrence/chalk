namespace Server.Common.Utilities;

internal static class FileUtilities
{
  public static bool IsAcceptedImage(this string contentType)
  {
    return contentType switch
    {
      "image/jpeg" or "image/png" => true,
      _ => false
    };
  }
}
