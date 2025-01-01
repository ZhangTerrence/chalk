using System.Security.Claims;
using chalk.Server.Configurations;
using chalk.Server.Data;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
using chalk.Server.Shared;
using Microsoft.EntityFrameworkCore;

namespace chalk.Server.Services;

public class UserService : IUserService
{
    private readonly DatabaseContext _context;

    public UserService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserResponse>> GetUsersAsync()
    {
        return await _context.Users
            .Include(e => e.UserOrganizations).ThenInclude(e => e.Organization)
            .Include(e => e.UserCourses).ThenInclude(e => e.Course)
            .Include(e => e.ChannelParticipants).ThenInclude(e => e.Channel)
            .Select(e => e.ToResponse())
            .ToListAsync();
    }

    public async Task<UserResponse> GetUserAsync(long userId)
    {
        var user = await _context.Users
            .Include(e => e.UserOrganizations).ThenInclude(e => e.Organization)
            .Include(e => e.UserCourses).ThenInclude(e => e.Course)
            .Include(e => e.ChannelParticipants).ThenInclude(e => e.Channel)
            .FirstOrDefaultAsync(e => e.Id == userId);
        if (user is null)
        {
            throw new ServiceException("User not found.", StatusCodes.Status404NotFound);
        }

        return user.ToResponse();
    }

    public async Task<IEnumerable<InviteResponse>> GetPendingInvitesAsync(ClaimsPrincipal identity, long userId)
    {
        var user = await _context.Users.FindAsync(identity);
        if (user is null)
        {
            throw new ServiceException("User not found.", StatusCodes.Status404NotFound);
        }

        var userRoles = await Task.WhenAll(
            _context.UserRoles
                .Where(e => e.UserId == user.Id).AsEnumerable()
                .Select(async e => (await _context.Roles.FindAsync(e.RoleId))?.Name)
                .ToList());
        if (!userRoles.Contains("Admin") && user.Id != userId)
        {
            throw new ServiceException("Unauthorized to read invites.", StatusCodes.Status403Forbidden);
        }

        var invites = new List<InviteResponse>();
        invites.AddRange(_context.UserOrganizations
            .Where(e => e.UserId == userId && e.Status == Status.Invited)
            .Select(e => e.ToResponse()));
        invites.AddRange(_context.UserCourses
            .Where(e => e.UserId == userId && e.Status == Status.Invited)
            .Select(e => e.ToResponse()));

        return invites;
    }

    public async Task RespondToInviteAsync(RespondToInviteRequest respondToInviteRequest)
    {
        var invite = await _context.UserOrganizations
            .Include(e => e.Organization)
            .FirstOrDefaultAsync(e =>
                e.UserId == respondToInviteRequest.UserId && e.OrganizationId == respondToInviteRequest.OrganizationId);
        if (invite is null)
        {
            throw new ServiceException("Invite not found.", StatusCodes.Status404NotFound);
        }

        switch (respondToInviteRequest.Invite)
        {
            case Invite.Organization:
                await OrganizationInviteResponse(invite, respondToInviteRequest);
                break;
            case Invite.Course:
                break;
            default:
                throw new ServiceException("Unrecognized invite type.", StatusCodes.Status400BadRequest);
        }
    }

    private async Task OrganizationInviteResponse(
        UserOrganization invite,
        RespondToInviteRequest respondToInviteRequest
    )
    {
        switch (invite.Status)
        {
            case Status.User:
                throw new ServiceException("User has already joined.", StatusCodes.Status409Conflict);
            case Status.Banned:
                throw new ServiceException("User is banned.", StatusCodes.Status403Forbidden);
            case Status.Invited:
                break;
            default:
                throw new ServiceException("Unrecognized invite status.", StatusCodes.Status400BadRequest);
        }

        if (respondToInviteRequest.Accept)
        {
            invite.Status = Status.User;
            invite.JoinedDate = DateTime.UtcNow;
        }
        else
        {
            _context.UserOrganizations.Remove(invite);
        }

        await _context.SaveChangesAsync();
    }
}