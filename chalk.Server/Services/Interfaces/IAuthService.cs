using System.Security.Claims;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;

namespace chalk.Server.Services.Interfaces;

public interface IAuthService
{
    public Task<AuthResponse> RegisterUserAsync(RegisterRequest registerRequest);

    public Task<AuthResponse> AuthenticateUserAsync(LoginRequest loginRequest);

    public Task LogoutUserAsync(ClaimsPrincipal identity);

    public Task RefreshTokensAsync(ClaimsPrincipal identity, string? refreshToken);
}