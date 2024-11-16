using chalk.Server.Entities;

namespace chalk.Server.DTOs;

public record UserResponseDTO
{
    public UserResponseDTO(User user)
    {
        Email = user.Email!;
        DisplayName = user.DisplayName;
    }

    public string Email { get; init; }
    public string DisplayName { get; init; }
}