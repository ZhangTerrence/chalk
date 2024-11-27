using chalk.Server.DTOs.User;

namespace chalk.Server.Services.Interfaces;

public interface IUserService
{
    public Task<UserDTO> RegisterUserAsync(RegisterDTO registerDTO);

    public Task<(UserDTO, IEnumerable<string>)> AuthenticateUserAsync(LoginDTO loginDTO);

    public Task AddRefreshTokenAsync(long userId, string refreshToken);
}