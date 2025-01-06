using System.Security.Claims;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;

namespace chalk.Server.Services.Interfaces;

public interface IAuthenticationService
{
    public Task<AuthenticationResponse> RegisterUserAsync(RegisterRequest request);

    public Task<AuthenticationResponse> LoginUserAsync(LoginRequest request);

    public Task<AuthenticationResponse> RefreshTokensAsync(string? accessToken, string? refreshToken);

    public Task LogoutUserAsync(ClaimsPrincipal identity);
}