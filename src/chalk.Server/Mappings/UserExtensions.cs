using System.Globalization;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;

namespace chalk.Server.Mappings;

public static class UserExtensions
{
    public static User ToEntity(this RegisterRequest registerRequest)
    {
        return new User
        {
            Email = registerRequest.Email!,
            FirstName = registerRequest.FirstName!,
            LastName = registerRequest.LastName!,
            DisplayName = registerRequest.DisplayName!,
            UserName = registerRequest.Email!,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };
    }

    public static UserResponse ToDTO(this User user)
    {
        return new UserResponse(
            user.Id,
            user.Email!,
            user.FirstName,
            user.LastName,
            user.DisplayName,
            user.ProfilePicture,
            user.CreatedDate.ToString(CultureInfo.CurrentCulture),
            user.UpdatedDate.ToString(CultureInfo.CurrentCulture),
            user.DirectMessages
                .Select(e => new ChannelDTO(e.Channel))
                .ToList(),
            user.Organizations
                .Select(e => new OrganizationDTO(e.Organization))
                .ToList(),
            user.Courses
                .Select(e => new CourseDTO(e.Course))
                .ToList()
        );
    }
}