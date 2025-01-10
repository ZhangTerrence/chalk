using chalk.Server.DTOs.Requests;
using chalk.Server.Entities;

namespace chalk.Server.Services.Interfaces;

public interface IAuthService
{
    public Task<(long, string, string)> CreateTokensAsync(User user, IEnumerable<string> roles);

    public Task<(long, string, string)> AuthenticateAsync(LoginRequest request);

    public Task<(long, string, string)> RefreshTokenAsync(string? accessToken, string? refreshToken);

    public Task DeleteTokensAsync(long userId);
}