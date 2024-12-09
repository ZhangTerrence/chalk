using chalk.Server.DTOs.User;

namespace chalk.Server.Services.Interfaces;

public interface IUserService
{
    public Task<(UserResponseDTO, string, string)> RegisterUserAsync(RegisterDTO registerDTO);

    public Task<(UserResponseDTO, string, string)> AuthenticateUserAsync(LoginDTO loginDTO);

    public Task<IEnumerable<UserResponseDTO>> GetUsersAsync();

    public Task<UserResponseDTO> GetUserAsync(long userId);

    public Task<IEnumerable<InviteDTO>> GetPendingInvitesAsync(long userId);

    public Task RespondInviteAsync(InviteResponseDTO inviteResponseDTO);
}