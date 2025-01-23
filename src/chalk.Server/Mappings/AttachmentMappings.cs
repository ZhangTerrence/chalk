using System.Globalization;
using chalk.Server.DTOs;
using chalk.Server.Entities;

namespace chalk.Server.Mappings;

public static class AttachmentMappings
{
    public static AttachmentDTO ToDTO(this Attachment attachment)
    {
        return new AttachmentDTO(
            attachment.Id,
            attachment.Name,
            attachment.Description,
            attachment.Resource,
            attachment.CreatedDate.ToString(CultureInfo.CurrentCulture),
            attachment.UpdatedDate.ToString(CultureInfo.CurrentCulture)
        );
    }
}