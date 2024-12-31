using System.Security.Claims;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;

namespace chalk.Server.Services.Interfaces;

public interface IAuthenticationService
{
    public Task<AuthenticationResponse> RegisterUserAsync(RegisterRequest registerRequest);

    public Task<AuthenticationResponse> LoginUserAsync(LoginRequest loginRequest);

    public Task LogoutUserAsync(ClaimsPrincipal identity);

    public Task RefreshTokensAsync(ClaimsPrincipal identity, string? refreshToken);
}