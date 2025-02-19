using System.Globalization;
using Server.Common.Requests.File;
using Server.Common.Responses;
using File = Server.Data.Entities.File;

namespace Server.Common.Mappings;

internal static class FileMappings
{
  public static FileResponse ToResponse(this File file)
  {
    return new FileResponse(
      file.Id,
      file.Name,
      file.Description,
      file.FileUrl,
      file.CreatedOnUtc.ToString(CultureInfo.CurrentCulture),
      file.UpdatedOnUtc.ToString(CultureInfo.CurrentCulture)
    );
  }

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
}
