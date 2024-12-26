using System.Security.Claims;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;

namespace chalk.Server.Services.Interfaces;

public interface IUserService
{
    public Task<IEnumerable<UserResponse>> GetUsersAsync();

    public Task<UserResponse> GetUserAsync(long userId);

    public Task<IEnumerable<InviteResponse>> GetPendingInvitesAsync(ClaimsPrincipal identity, long userId);

    public Task RespondToInviteAsync(RespondToInviteRequest respondToInviteRequest);
}