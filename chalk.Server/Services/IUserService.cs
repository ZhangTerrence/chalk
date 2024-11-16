using chalk.Server.DTOs;
using chalk.Server.Entities;

namespace chalk.Server.Services;

public interface IUserService
{
    public Task<User> RegisterUserAsync(RegisterRequestDTO registerRequestDTO);
    public Task<(User, IEnumerable<string>)> AuthenticateUserAsync(LoginRequestDTO loginRequestDTO);
    public Task AddRefreshTokenAsync(User user, string refreshToken);
}