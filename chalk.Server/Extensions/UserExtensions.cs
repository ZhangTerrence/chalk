using chalk.Server.DTOs;
using chalk.Server.Entities;

namespace chalk.Server.Extensions;

public static class UserExtensions
{
    public static User ToUser(this RegisterRequestDTO registerRequestDTO)
    {
        return new User
        {
            UserName = registerRequestDTO.Email,
            Email = registerRequestDTO.Email,
            DisplayName = registerRequestDTO.DisplayName,
        };
    }

    public static UserResponseDTO ToUserResponseDTO(this User user)
    {
        return new UserResponseDTO(user);
    }
}