using Server.Data.Entities;
using LoginRequest = Server.Common.Requests.Identity.LoginRequest;
using RegisterRequest = Server.Common.Requests.Identity.RegisterRequest;

namespace Server.Common.Interfaces;

public interface IIdentityService
{
  public Task<string> CreateUserAsync(RegisterRequest request);

  public Task<User> LoginUserAsync(LoginRequest request);

  public Task<User> RefreshUserAsync(long userId);

  public Task LogoutUserAsync();

  public Task ConfirmEmailAsync(string email, string token);
}
