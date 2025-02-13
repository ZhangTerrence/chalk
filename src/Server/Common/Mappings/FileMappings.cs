using System.Globalization;
using Server.Common.DTOs;
using Server.Common.Requests.File;
using File = Server.Data.Entities.File;

namespace Server.Common.Mappings;

internal static class FileMappings
{
  public static File ToEntity(this CreateRequest request, string fileUrl)
  {
    return new File
    {
      Name = request.Name,
      Description = request.Description,
      FileUrl = fileUrl,
      CreatedOnUtc = DateTime.UtcNow,
      UpdatedOnUtc = DateTime.UtcNow
    };
  }

  public static FileDto ToDto(this File file)
  {
    return new FileDto(
      file.Id,
      file.Name,
      file.Description,
      file.FileUrl,
      file.CreatedOnUtc.ToString(CultureInfo.CurrentCulture),
      file.UpdatedOnUtc.ToString(CultureInfo.CurrentCulture)
    );
  }
}
