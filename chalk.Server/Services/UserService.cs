using System.Security.Claims;
using chalk.Server.Common;
using chalk.Server.Data;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;
using chalk.Server.Extensions;
using chalk.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace chalk.Server.Services;

public class UserService : IUserService
{
    private readonly DatabaseContext _context;

    public UserService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserResponseDTO>> GetUsersAsync()
    {
        return await _context.Users
            .Include(e => e.UserOrganizations).ThenInclude(e => e.Organization)
            .Include(e => e.UserCourses).ThenInclude(e => e.Course)
            .Include(e => e.ChannelParticipants).ThenInclude(e => e.Channel)
            .Select(e => e.ToUserResponseDTO())
            .ToListAsync();
    }

    public async Task<UserResponseDTO> GetUserAsync(long userId)
    {
        var user = await _context.Users
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
            throw new BadHttpRequestException("Unauthorized.", StatusCodes.Status401Unauthorized);
        }

        var currentUser = await _context.Users.FindAsync(long.Parse(currentUserId));
        if (currentUser is null)
        {
            throw new BadHttpRequestException("User not found.", StatusCodes.Status404NotFound);
        }

        var currentUserRoles = await Task.WhenAll(
            _context.UserRoles
                .Where(e => e.UserId == currentUser.Id).AsEnumerable()
                .Select(async e => (await _context.Roles.FindAsync(e.RoleId))?.Name)
                .ToList());
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