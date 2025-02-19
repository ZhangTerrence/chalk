using System.Globalization;
using Server.Common.Requests.AssignmentGroup;
using Server.Common.Responses;
using Server.Data.Entities;

namespace Server.Common.Mappings;

internal static class AssignmentGroupMappings
{
  public static AssignmentGroupResponse ToResponse(this AssignmentGroup assignmentGroup)
  {
    return new AssignmentGroupResponse(
      assignmentGroup.Id,
      assignmentGroup.Name,
      assignmentGroup.Description,
      assignmentGroup.Weight,
      assignmentGroup.CreatedOnUtc.ToString(CultureInfo.CurrentCulture),
      assignmentGroup.UpdatedOnUtc.ToString(CultureInfo.CurrentCulture),
      assignmentGroup.Assignments.Select(e => e.ToResponse())
    );
  }

  public static AssignmentGroup ToEntity(this CreateRequest request, long courseId)
  {
    return new AssignmentGroup
    {
      Name = request.Name,
      Description = request.Description,
      Weight = request.Weight!.Value,
      CreatedOnUtc = DateTime.UtcNow,
      UpdatedOnUtc = DateTime.UtcNow,
      CourseId = courseId
    };
  }
}
