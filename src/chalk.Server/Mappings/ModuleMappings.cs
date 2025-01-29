using System.Globalization;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Requests;
using chalk.Server.Entities;

namespace chalk.Server.Mappings;

public static class ModuleMappings
{
    public static Module ToEntity(this CreateModuleRequest request, int relativeOrder)
    {
        return new Module
        {
            Name = request.Name,
            Description = request.Description,
            RelativeOrder = relativeOrder,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
    }

    public static ModuleDTO ToDTO(this Module module)
    {
        return new ModuleDTO(
            module.Id,
            module.Name,
            module.Description,
            module.RelativeOrder,
            module.CreatedDate.ToString(CultureInfo.CurrentCulture),
            module.Files.Select(e => e.ToDTO())
        );
    }
}