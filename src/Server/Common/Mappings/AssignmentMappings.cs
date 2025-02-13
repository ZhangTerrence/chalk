using System.Globalization;
using Server.Common.DTOs;
using Server.Common.Requests.Assignment;
using Server.Data.Entities;

namespace Server.Common.Mappings;

internal static class AssignmentMappings
{
  public static Assignment ToEntity(this CreateRequest request, long assignmentGroupId)
  {
    return new Assignment
    {
      Name = request.Name,
      Description = request.Description,
      DueOnUtc = request.DueOnUtc,
      CreatedOnUtc = DateTime.UtcNow,
      UpdatedOnUtc = DateTime.UtcNow,
      AssignmentGroupId = assignmentGroupId
    };
  }

  public static AssignmentDto ToDto(this Assignment assignment)
  {
    return new AssignmentDto(
      assignment.Id,
      assignment.Name,
      assignment.Description,
      assignment.DueOnUtc?.ToString(CultureInfo.CurrentCulture),
      assignment.CreatedOnUtc.ToString(CultureInfo.CurrentCulture),
      assignment.UpdatedOnUtc.ToString(CultureInfo.CurrentCulture),
      assignment.Files.Select(e => e.ToDto())
    );
  }
}
