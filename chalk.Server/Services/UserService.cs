using System.Security.Claims;
using chalk.Server.Common;
using chalk.Server.Data;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Responses;
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

    public async Task<AuthResponseDTO> RegisterUserAsync(RegisterDTO registerDTO)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerDTO.Email);
        if (existingUser is not null)
        {
            throw new BadHttpRequestException("User already exists.", StatusCodes.Status409Conflict);
        }

        var user = registerDTO.ToUser();

        var createdUser = await _userManager.CreateAsync(user, registerDTO.Password);
        if (!createdUser.Succeeded)
        {
            throw new BadHttpRequestException("Unable to create user.", StatusCodes.Status500InternalServerError);
        }

        if (!await _roleManager.RoleExistsAsync("User"))
        {
            var createdRole = await _roleManager.CreateAsync(new IdentityRole<long>("User"));
            if (!createdRole.Succeeded)
            {
                throw new BadHttpRequestException("Unable to create 'User' role.",
                    StatusCodes.Status500InternalServerError);
            }
        }

        var assignedUser = await _userManager.AddToRoleAsync(user, "User");
        if (!assignedUser.Succeeded)
        {
            throw new BadHttpRequestException("Unable to assign user to user role.",
                StatusCodes.Status500InternalServerError);
        }

        var accessToken = _tokenService.CreateAccessToken(user.Id, user.DisplayName, ["User"]);
        var refreshToken = _tokenService.CreateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryDate = DateTime.Now.AddDays(1).ToUniversalTime();
        var updatedUser = await _userManager.UpdateAsync(user);
        if (!updatedUser.Succeeded)
        {
            throw new BadHttpRequestException("Unable to set refresh token.", StatusCodes.Status500InternalServerError);
        }

        return new AuthResponseDTO(accessToken, refreshToken);
    }

    public async Task<AuthResponseDTO> AuthenticateUserAsync(LoginDTO loginDTO)
    {
        var user = await _userManager.FindByEmailAsync(loginDTO.Email);
        if (user is null)
        {
            throw new BadHttpRequestException("User not found.", StatusCodes.Status404NotFound);
        }

        var authenticated = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
        if (!authenticated)
        {
            throw new BadHttpRequestException("Invalid credentials.", StatusCodes.Status401Unauthorized);
        }

        var roles = await _userManager.GetRolesAsync(user);

        var accessToken = _tokenService.CreateAccessToken(user.Id, user.DisplayName, roles);
        var refreshToken = _tokenService.CreateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryDate = DateTime.Now.AddDays(1).ToUniversalTime();
        var updatedUser = await _userManager.UpdateAsync(user);
        if (!updatedUser.Succeeded)
        {
            throw new BadHttpRequestException("Unable to set refresh token.", StatusCodes.Status500InternalServerError);
        }

        return new AuthResponseDTO(accessToken, refreshToken);
    }

    public async Task<IEnumerable<UserResponseDTO>> GetUsersAsync()
    {
        return await _userManager.Users
            .Include(e => e.UserOrganizations).ThenInclude(e => e.Organization)
            .Include(e => e.UserCourses).ThenInclude(e => e.Course)
            .Include(e => e.ChannelParticipants).ThenInclude(e => e.Channel)
            .Select(e => e.ToUserResponseDTO())
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

        return user.ToUserResponseDTO();
    }

    public async Task<IEnumerable<InviteResponseDTO>> GetPendingInvitesAsync(long userId, ClaimsPrincipal authUser)
    {
        var currentUserId = authUser.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserId is null)
        {
            throw new BadHttpRequestException("Unauthorized.", StatusCodes.Status403Forbidden);
        }

        var currentUser = await _userManager.FindByIdAsync(currentUserId);
        if (currentUser is null)
        {
            throw new BadHttpRequestException("User not found.", StatusCodes.Status404NotFound);
        }

        var currentUserRoles = await _userManager.GetRolesAsync(currentUser);
        if (!currentUserRoles.Contains("Admin") && currentUser.Id != userId)
        {
            throw new BadHttpRequestException("Unauthorized.", StatusCodes.Status403Forbidden);
        }

        var invites = new List<InviteResponseDTO>();
        invites.AddRange(_context.UserOrganizations
            .Where(e => e.UserId == userId && e.Status == MemberStatus.Invited)
            .Select(e => e.ToInviteResponseDTO()));
        invites.AddRange(_context.UserCourses
            .Where(e => e.UserId == userId && e.Status == MemberStatus.Invited)
            .Select(e => e.ToInviteResponseDTO()));

        return invites;
    }

    public async Task RespondToInviteAsync(RespondToInviteDTO respondToInviteDTO)
    {
        var invite = await _context.UserOrganizations
            .Include(e => e.Organization)
            .FirstOrDefaultAsync(e =>
                e.UserId == respondToInviteDTO.UserId && e.OrganizationId == respondToInviteDTO.OrganizationId);
        if (invite is null)
        {
            throw new BadHttpRequestException("Invite not found.", StatusCodes.Status404NotFound);
        }

        switch (respondToInviteDTO.InviteType)
        {
            case InviteType.Organization:
                await ResponseOrganizationInvite(invite, respondToInviteDTO);
                break;
            case InviteType.Course:
                break;
            default:
                throw new BadHttpRequestException("Invalid invite type.", StatusCodes.Status400BadRequest);
        }
    }

    private async Task ResponseOrganizationInvite(UserOrganization invite, RespondToInviteDTO respondToInviteDTO)
    {
        switch (invite.Status)
        {
            case MemberStatus.User:
                throw new BadHttpRequestException("User already in organization.", StatusCodes.Status409Conflict);
            case MemberStatus.Banned:
                throw new BadHttpRequestException("User is banned.", StatusCodes.Status403Forbidden);
            case MemberStatus.Invited:
                break;
            default:
                throw new BadHttpRequestException("Invalid status.", StatusCodes.Status400BadRequest);
        }

        if (respondToInviteDTO.Accept)
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