using System.Globalization;
using chalk.Server.DTOs.User;
using chalk.Server.Entities;

namespace chalk.Server.Extensions;

public static class UserExtensions
{
    public static User ToUser(this RegisterDTO registerDTO)
    {
        return new User
        {
            Email = registerDTO.Email,
            FirstName = registerDTO.FirstName,
            LastName = registerDTO.LastName,
            FullName = $"{registerDTO.FirstName} {registerDTO.LastName}",
            DisplayName = registerDTO.DisplayName,
            UserName = registerDTO.Email,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };
    }

    public static UserResponseDTO ToUserResponseDTO(this User user)
    {
        return new UserResponseDTO(
            user.Id,
            user.Email,
            user.FullName,
            user.DisplayName,
            user.ProfilePicture,
            user.CreatedDate.ToString(CultureInfo.CurrentCulture),
            user.UpdatedDate.ToString(CultureInfo.CurrentCulture),
            user.UserOrganizations
                .Select(e => new UserResponseDTO.OrganizationDTO(e.Organization.Id, e.Organization.Name))
                .ToList(),
            user.UserCourses
                .Select(e => new UserResponseDTO.CourseDTO(e.Course.Id, e.Course.Name, e.Course.Code))
                .ToList(),
            user.ChannelParticipants
                .Select(e => new UserResponseDTO.ChannelDTO(e.Channel.Id, e.Channel.Name))
                .ToList()
        );
    }
}