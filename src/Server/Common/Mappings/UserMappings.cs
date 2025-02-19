using System.Globalization;
using Server.Common.Requests.Identity;
using Server.Common.Responses;
using Server.Data.Entities;

namespace Server.Common.Mappings;

internal static class UserMappings
{
  public static UserResponse ToResponse(this User user)
  {
    return new UserResponse(
      user.Id,
      user.Email!,
      user.FirstName,
      user.LastName,
      user.DisplayName,
      user.Description,
      user.ImageUrl,
      user.CreatedOnUtc.ToString(CultureInfo.CurrentCulture),
      user.UpdatedOnUtc.ToString(CultureInfo.CurrentCulture),
      user.DirectMessages.Select(e => new UserResponse.PartialChannelResponse(e.Channel.Id)),
      user.Courses.Select(e => new UserResponse.PartialCourseResponse(e.Course.Id, e.Course.Name, e.Course.Code)),
      user.Organizations.Select(e =>
        new UserResponse.PartialOrganizationResponse(e.Organization.Id, e.Organization.Name))
    );
  }

  public static User ToEntity(this RegisterRequest registerRequest)
  {
    return new User
    {
      Email = registerRequest.Email,
      FirstName = registerRequest.FirstName,
      LastName = registerRequest.LastName,
      DisplayName = registerRequest.DisplayName,
      UserName = registerRequest.Email,
      CreatedOnUtc = DateTime.UtcNow,
      UpdatedOnUtc = DateTime.UtcNow
    };
  }
}
