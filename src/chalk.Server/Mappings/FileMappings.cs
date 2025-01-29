using System.Globalization;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Requests;
using File = chalk.Server.Entities.File;

namespace chalk.Server.Mappings;

public static class FileMappings
{
    public static File ToEntity(this CreateFileRequest request, string fileUrl)
    {
        return new File
        {
            Name = request.Name,
            Description = request.Description,
            FileUrl = fileUrl,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
    }

    public static FileDTO ToDTO(this File attachment)
    {
        return new FileDTO(
            attachment.Id,
            attachment.Name,
            attachment.Description,
            attachment.FileUrl,
            attachment.CreatedDate.ToString(CultureInfo.CurrentCulture),
            attachment.UpdatedDate.ToString(CultureInfo.CurrentCulture)
        );
    }
}