using System.Globalization;
using Server.Common.DTOs;
using Server.Common.Requests.AssignmentGroup;
using Server.Data.Entities;

namespace Server.Common.Mappings;

internal static class AssignmentGroupMappings
{
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

  public static AssignmentGroupDto ToDto(this AssignmentGroup assignmentGroup)
  {
    return new AssignmentGroupDto(
      assignmentGroup.Id,
      assignmentGroup.Name,
      assignmentGroup.Description,
      assignmentGroup.Weight,
      assignmentGroup.CreatedOnUtc.ToString(CultureInfo.CurrentCulture),
      assignmentGroup.Assignments.Select(e => e.ToDto())
    );
  }
}
