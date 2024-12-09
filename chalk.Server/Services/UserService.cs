using chalk.Server.Common;
using chalk.Server.Data;
using chalk.Server.DTOs.User;
using chalk.Server.Entities;
using chalk.Server.Extensions;
using chalk.Server.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace chalk.Server.Services;

public class UserService : IUserService
{
    private readonly DatabaseContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    private readonly ITokenService _tokenService;

    public UserService(
        DatabaseContext context,
        UserManager<User> userManager,
        RoleManager<IdentityRole<long>> roleManager,
        ITokenService tokenService
    )
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
    }

    public async Task<(UserResponseDTO, string, string)> RegisterUserAsync(RegisterDTO registerDTO)
    {
        if (await _userManager.Users.FirstOrDefaultAsync(e => e.Email == registerDTO.Email) is not null)
        {
            throw new BadHttpRequestException("User already exists.", StatusCodes.Status400BadRequest);
        }

        var user = registerDTO.ToUser();

        var createdUser = await _userManager.CreateAsync(user, registerDTO.Password);
        if (!createdUser.Succeeded)
        {
            throw new BadHttpRequestException("Unable to create user.", StatusCodes.Status500InternalServerError);
        }

        if (!await _roleManager.RoleExistsAsync(nameof(Role.User)))
        {
            var createdRole = await _roleManager.CreateAsync(new IdentityRole<long>(nameof(Role.User)));
            if (!createdRole.Succeeded)
            {
                throw new BadHttpRequestException("Unable to create 'User' role.",
                    StatusCodes.Status500InternalServerError);
            }
        }

        var assignedUser = await _userManager.AddToRoleAsync(user, nameof(Role.User));
        if (!assignedUser.Succeeded)
        {
            throw new BadHttpRequestException("Unable to assign user to user role.",
                StatusCodes.Status500InternalServerError);
        }

        var accessToken = _tokenService.CreateAccessToken(user.Id, user.FullName, ["User"]);
        var refreshToken = _tokenService.CreateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryDate = DateTime.Now.AddDays(1).ToUniversalTime();
        var updatedUser = await _userManager.UpdateAsync(user);
        if (!updatedUser.Succeeded)
        {
            throw new BadHttpRequestException("Unable to update user.", StatusCodes.Status500InternalServerError);
        }

        return (user.ToUserDTO(), accessToken, refreshToken);
    }

    public async Task<(UserResponseDTO, string, string)> AuthenticateUserAsync(LoginDTO loginDTO)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(e => e.Email == loginDTO.Email);
        if (user is null)
        {
            throw new BadHttpRequestException("User not found.", StatusCodes.Status404NotFound);
        }

        var authenticated = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
        if (!authenticated)
        {
            throw new BadHttpRequestException("Invalid email or password.", StatusCodes.Status401Unauthorized);
        }

        var roles = await _userManager.GetRolesAsync(user);

        var accessToken = _tokenService.CreateAccessToken(user.Id, user.FullName, roles);
        var refreshToken = _tokenService.CreateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryDate = DateTime.Now.AddDays(1).ToUniversalTime();
        var updatedUser = await _userManager.UpdateAsync(user);
        if (!updatedUser.Succeeded)
        {
            throw new BadHttpRequestException("Unable to update user.", StatusCodes.Status500InternalServerError);
        }

        return (user.ToUserDTO(), accessToken, refreshToken);
    }

    public async Task<IEnumerable<UserResponseDTO>> GetUsersAsync()
    {
        return await _userManager.Users
            .Include(e => e.UserOrganizations).ThenInclude(e => e.Organization)
            .Include(e => e.UserCourses).ThenInclude(e => e.Course)
            .Include(e => e.ChannelParticipants).ThenInclude(e => e.Channel)
            .Select(e => e.ToUserDTO())
            .ToListAsync();
    }

    public async Task<UserResponseDTO> GetUserAsync(long userId)
    {
        var user = await _userManager.Users
            .Include(e => e.UserOrganizations).ThenInclude(e => e.Organization)
            .Include(e => e.UserCourses).ThenInclude(e => e.Course)
            .Include(e => e.ChannelParticipants).ThenInclude(e => e.Channel)
            .FirstOrDefaultAsync(e => e.Id == userId);
        if (user is null)
        {
            throw new BadHttpRequestException("User not found.", StatusCodes.Status404NotFound);
        }

        return user.ToUserDTO();
    }

    public async Task<IEnumerable<InviteDTO>> GetPendingInvitesAsync(long userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null)
        {
            throw new BadHttpRequestException("User not found.", StatusCodes.Status404NotFound);
        }

        var invites = new List<InviteDTO>();
        invites.AddRange(_context.UserOrganizations
            .Where(e => e.UserId == userId && e.Status == MemberStatus.Invited)
            .Select(e => e.ToInviteDTO()));
        invites.AddRange(_context.UserCourses
            .Where(e => e.UserId == userId && e.Status == MemberStatus.Invited)
            .Select(e => e.ToInviteDTO()));

        return invites;
    }

    public async Task RespondInviteAsync(InviteResponseDTO inviteResponseDTO)
    {
        var invite = await _context.UserOrganizations
            .Include(e => e.Organization)
            .FirstOrDefaultAsync(e =>
                e.UserId == inviteResponseDTO.UserId && e.OrganizationId == inviteResponseDTO.OrganizationId);
        if (invite is null)
        {
            throw new BadHttpRequestException("Invite not found.", StatusCodes.Status404NotFound);
        }

        switch (inviteResponseDTO.InviteType)
        {
            case InviteType.Organization:
                await ResponseOrganizationInvite(invite, inviteResponseDTO);
                break;
            case InviteType.Course:
                break;
            default:
                throw new BadHttpRequestException("Invalid invite type.", StatusCodes.Status400BadRequest);
        }
    }

    private async Task ResponseOrganizationInvite(UserOrganization invite, InviteResponseDTO inviteResponseDTO)
    {
        switch (invite.Status)
        {
            case MemberStatus.User:
                throw new BadHttpRequestException("Already user.", StatusCodes.Status400BadRequest);
            case MemberStatus.Banned:
                throw new BadHttpRequestException("User is banned.", StatusCodes.Status400BadRequest);
            case MemberStatus.Invited:
                break;
            default:
                throw new BadHttpRequestException("Invalid status.", StatusCodes.Status400BadRequest);
        }

        if (inviteResponseDTO.Accept)
        {
            invite.Status = MemberStatus.User;
            invite.JoinedDate = DateTime.UtcNow;
        }
        else
        {
            _context.UserOrganizations.Remove(invite);
        }

        await _context.SaveChangesAsync();
    }
}