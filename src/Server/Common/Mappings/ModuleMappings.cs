using System.Globalization;
using Server.Common.DTOs;
using Server.Common.Requests.Module;
using Server.Data.Entities;

namespace Server.Common.Mappings;

internal static class ModuleMappings
{
  public static Module ToEntity(this CreateRequest request, int relativeOrder)
  {
    return new Module
    {
      Name = request.Name,
      Description = request.Description,
      RelativeOrder = relativeOrder,
      CreatedOnUtc = DateTime.UtcNow,
      UpdatedOnUtc = DateTime.UtcNow
    };
  }

  public static ModuleDto ToDto(this Module module)
  {
    return new ModuleDto(
      module.Id,
      module.Name,
      module.Description,
      module.RelativeOrder,
      module.CourseId.ToString(CultureInfo.CurrentCulture),
      module.Files.Select(e => e.ToDto())
    );
  }
}
