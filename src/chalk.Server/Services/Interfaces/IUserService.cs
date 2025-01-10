using chalk.Server.DTOs.Requests;
using chalk.Server.Entities;

namespace chalk.Server.Services.Interfaces;

public interface IUserService
{
    public Task<IEnumerable<User>> GetUsersAsync();

    public Task<User> GetUserAsync(long userId);

    public Task<User> CreateUserAsync(RegisterRequest request);
}