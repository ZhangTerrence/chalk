using System.Security.Claims;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Responses;

namespace chalk.Server.Services.Interfaces;

public interface IUserService
{
    public Task<AuthResponseDTO> RegisterUserAsync(RegisterDTO registerDTO);

    public Task<AuthResponseDTO> AuthenticateUserAsync(LoginDTO loginDTO);

    public Task<IEnumerable<UserResponseDTO>> GetUsersAsync();

    public Task<UserResponseDTO> GetUserAsync(long userId);

    public Task<IEnumerable<InviteResponseDTO>> GetPendingInvitesAsync(long userId, ClaimsPrincipal authUser);

    public Task RespondToInviteAsync(RespondToInviteDTO respondToInviteDTO);
}