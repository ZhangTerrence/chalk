using System.Globalization;
using Server.Common.Requests.Module;
using Server.Common.Responses;
using Server.Data.Entities;

namespace Server.Common.Mappings;

internal static class ModuleMappings
{
  public static ModuleResponse ToResponse(this Module module)
  {
    return new ModuleResponse(
      module.Id,
      module.Name,
      module.Description,
      module.RelativeOrder,
      module.CreatedOnUtc.ToString(CultureInfo.CurrentCulture),
      module.UpdatedOnUtc.ToString(CultureInfo.CurrentCulture),
      module.Files.Select(e => e.ToResponse())
    );
  }

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
}
