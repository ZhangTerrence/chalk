using System.Globalization;
using Server.Common.Requests.Assignment;
using Server.Common.Responses;
using Server.Data.Entities;

namespace Server.Common.Mappings;

internal static class AssignmentMappings
{
  public static AssignmentResponse ToResponse(this Assignment assignment)
  {
    return new AssignmentResponse(
      assignment.Id,
      assignment.Name,
      assignment.Description,
      assignment.DueOnUtc?.ToString(CultureInfo.CurrentCulture),
      assignment.CreatedOnUtc.ToString(CultureInfo.CurrentCulture),
      assignment.UpdatedOnUtc.ToString(CultureInfo.CurrentCulture),
      assignment.Files.Select(e => e.ToResponse())
    );
  }

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
}
