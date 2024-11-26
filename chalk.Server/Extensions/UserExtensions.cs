using chalk.Server.DTOs.User;
using chalk.Server.Entities;

namespace chalk.Server.Extensions;

public static class UserExtensions
{
    public static User ToUser(this RegisterDTO registerDTO)
    {
        return new User
        {
            DisplayName = registerDTO.DisplayName,
            Email = registerDTO.Email,
            UserName = registerDTO.Email,
            Description = "",
            CreatedDate = DateTime.Now.ToUniversalTime(),
            UpdatedDate = DateTime.Now.ToUniversalTime()
        };
    }

    public static UserDTO ToUserResponseDTO(this User user)
    {
        return new UserDTO(user);
    }
}