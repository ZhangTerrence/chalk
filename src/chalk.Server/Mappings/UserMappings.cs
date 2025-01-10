using System.Globalization;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;

namespace chalk.Server.Mappings;

public static class UserMappings
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

    public static UserResponse ToResponse(this User user)
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
            user.DirectMessages.Select(e => e.Channel.ToDTO()),
            user.Organizations.Select(e => e.Organization.ToDTO()),
            user.Courses.Select(e => e.Course.ToDTO())
        );
    }

    public static UserDTO ToDTO(this User user, string? joinedDate)
    {
        return new UserDTO(user.Id, user.FirstName, user.LastName, user.DisplayName, joinedDate);
    }
}