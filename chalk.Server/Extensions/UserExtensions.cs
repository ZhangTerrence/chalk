using chalk.Server.DTOs;
using chalk.Server.Entities;

namespace chalk.Server.Extensions;

public static class UserExtensions
{
    public static User ToUser(this RegisterRequestDTO registerRequestDTO)
    {
        return new User
        {
            DisplayName = registerRequestDTO.DisplayName,
            Email = registerRequestDTO.Email,
            Description = "",
            CreatedDate = DateTime.Now.ToUniversalTime(),
            UpdatedDate = DateTime.Now.ToUniversalTime()
        };
    }

    public static UserResponseDTO ToUserResponseDTO(this User user)
    {
        return new UserResponseDTO(user);
    }
}