using chalk.Server.DTOs.Responses;

namespace chalk.Server.Services.Interfaces;

public interface IUserService
{
    public Task<IEnumerable<UserResponse>> GetUsersAsync();

    public Task<UserResponse> GetUserAsync(long userId);
}