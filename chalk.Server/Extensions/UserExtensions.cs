using chalk.Server.DTOs.User;
using chalk.Server.Entities;

namespace chalk.Server.Extensions;

public static class UserExtensions
{
    public static User ToUser(this RegisterDTO registerDTO)
    {
        return new User
        {
            FirstName = registerDTO.FirstName,
            LastName = registerDTO.LastName,
            FullName = $"{registerDTO.FirstName} {registerDTO.LastName}",
            DisplayName = registerDTO.DisplayName,
            Email = registerDTO.Email,
            UserName = registerDTO.Email,
            CreatedDate = DateTime.Now.ToUniversalTime(),
            UpdatedDate = DateTime.Now.ToUniversalTime()
        };
    }

    public static UserDTO ToUserDTO(this User user)
    {
        return new UserDTO(user);
    }
}