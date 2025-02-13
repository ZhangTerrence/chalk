using System.Globalization;
using Server.Common.DTOs;
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
      user.DirectMessages.Select(e => e.Channel.ToDto()),
      user.Courses.Select(e => e.Course.ToDto()),
      user.Organizations.Select(e => e.Organization.ToDto())
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

  public static UserDto ToDto(this User user, string? joinedDate)
  {
    return new UserDto(
      user.Id,
      user.FirstName,
      user.LastName,
      user.DisplayName,
      user.Description,
      user.ImageUrl,
      user.CreatedOnUtc.ToString(CultureInfo.CurrentCulture),
      joinedDate
    );
  }
}
