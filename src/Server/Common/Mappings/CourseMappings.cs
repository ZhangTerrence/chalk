using System.Globalization;
using Server.Common.DTOs;
using Server.Common.Requests.Course;
using Server.Common.Responses;
using Server.Data.Entities;

namespace Server.Common.Mappings;

public static class CourseMappings
{
  public static CourseResponse ToResponse(this Course course)
  {
    return new CourseResponse(
      course.Id,
      course.Name,
      course.Code,
      course.Description,
      course.ImageUrl,
      course.IsPublic,
      course.CreatedOnUtc.ToString(CultureInfo.CurrentCulture),
      course.Modules.Select(e => e.ToDto()),
      course.AssignmentGroups.Select(e => e.ToDto())
    );
  }

  public static Course ToEntity(this CreateRequest request, Organization? organization)
  {
    return new Course
    {
      Name = request.Name,
      Code = request.Code,
      Description = request.Description,
      IsPublic = request.IsPublic!.Value,
      CreatedOnUtc = DateTime.UtcNow,
      UpdatedOnUtc = DateTime.UtcNow,
      Organization = organization
    };
  }

  public static CourseDto ToDto(this Course course)
  {
    return new CourseDto(
      course.Id,
      course.Name,
      course.Code,
      course.Description,
      course.ImageUrl,
      course.IsPublic,
      course.CreatedOnUtc.ToString(CultureInfo.CurrentCulture)
    );
  }
}
