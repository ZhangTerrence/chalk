using System.Security.Claims;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Responses;

namespace chalk.Server.Services.Interfaces;

public interface IAuthService
{
    public Task<UserResponseDTO> RegisterUserAsync(RegisterDTO registerDTO);

    public Task<UserResponseDTO> AuthenticateUserAsync(LoginDTO loginDTO);

    public Task RefreshTokenAsync(ClaimsPrincipal identity, string? refreshToken);
}