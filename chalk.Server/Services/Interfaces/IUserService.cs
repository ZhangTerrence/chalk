using chalk.Server.DTOs.User;

namespace chalk.Server.Services.Interfaces;

public interface IUserService
{
    public Task<(UserDTO, string, string)> RegisterUserAsync(RegisterDTO registerDTO);

    public Task<(UserDTO, string, string)> AuthenticateUserAsync(LoginDTO loginDTO);

    public Task<IEnumerable<UserDTO>> GetUsersAsync();

    public Task<UserDTO> GetUserAsync(long userId);

    public Task<IEnumerable<InviteDTO>> GetPendingInvitesAsync(long userId);

    public Task RespondInviteAsync(InviteResponseDTO inviteResponseDTO);
}