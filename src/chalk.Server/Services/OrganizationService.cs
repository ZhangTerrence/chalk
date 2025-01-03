using System.Security.Claims;
using chalk.Server.Configurations;
using chalk.Server.Data;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
using chalk.Server.Shared;
using chalk.Server.Utilities;
using Microsoft.EntityFrameworkCore;

namespace chalk.Server.Services;

public class OrganizationService : IOrganizationService
{
    private readonly DatabaseContext _context;

    public OrganizationService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrganizationResponse>> GetOrganizationsAsync()
    {
        return await _context.Organizations
            .Include(e => e.Owner).AsSplitQuery()
            .Include(e => e.Users).ThenInclude(e => e.User).AsSplitQuery()
            .Include(e => e.Roles).AsSplitQuery()
            .Include(e => e.Courses).AsSplitQuery()
            .Select(e => e.ToResponse())
            .ToListAsync();
    }

    public async Task<OrganizationResponse> GetOrganizationAsync(long organizationId)
    {
        var organization = await _context.Organizations
            .Include(e => e.Owner).AsSplitQuery()
            .Include(e => e.Users).ThenInclude(e => e.User).AsSplitQuery()
            .Include(e => e.Roles).AsSplitQuery()
            .Include(e => e.Courses).AsSplitQuery()
            .FirstOrDefaultAsync(e => e.Id == organizationId);
        if (organization is null)
        {
            throw new ServiceException("Organization not found.", StatusCodes.Status404NotFound);
        }

        return organization.ToResponse();
    }

    public async Task<OrganizationResponse> CreateOrganizationAsync(CreateOrganizationRequest createOrganizationRequest)
    {
        var user = await _context.Users.FindAsync(createOrganizationRequest.OwnerId);
        if (user is null)
        {
            throw new ServiceException("User not found.", StatusCodes.Status404NotFound);
        }

        if (await _context.Organizations.AnyAsync(e => e.Name == createOrganizationRequest.Name))
        {
            throw new ServiceException("Organization already exists.", StatusCodes.Status409Conflict);
        }

        var organization = createOrganizationRequest.ToEntity(user);

        var ownerRole = new OrganizationRole
        {
            Name = "Owner",
            Permissions = PermissionUtilities.All,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };

        organization.Users.Add(new UserOrganization
        {
            Status = Status.User,
            JoinedDate = DateTime.UtcNow,
            User = user,
            Organization = organization,
            Role = ownerRole
        });

        organization.Roles.Add(ownerRole);

        var createdOrganization = await _context.Organizations.AddAsync(organization);

        await _context.SaveChangesAsync();
        return await GetOrganizationAsync(createdOrganization.Entity.Id);
    }

    public async Task<OrganizationResponse> UpdateOrganizationAsync(
        long organizationId,
        UpdateOrganizationRequest updateOrganizationRequest
    )
    {
        var organization = await _context.Organizations.FindAsync(organizationId);
        if (organization is null)
        {
            throw new ServiceException("Organization not found.", StatusCodes.Status404NotFound);
        }

        if (updateOrganizationRequest.Name is not null)
        {
            if (await _context.Organizations.AnyAsync(e => e.Name == updateOrganizationRequest.Name))
            {
                throw new ServiceException("Organization already exists.", StatusCodes.Status409Conflict);
            }

            organization.Name = updateOrganizationRequest.Name;
        }

        if (updateOrganizationRequest.Description is not null)
        {
            organization.Description = updateOrganizationRequest.Description;
        }

        organization.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return await GetOrganizationAsync(organizationId);
    }

    public async Task DeleteOrganizationAsync(long organizationId)
    {
        var organization = await _context.Organizations.FindAsync(organizationId);
        if (organization is null)
        {
            throw new ServiceException("Organization not found.", StatusCodes.Status404NotFound);
        }

        _context.Organizations.Remove(organization);

        await _context.SaveChangesAsync();
    }

    public async Task SendInviteAsync(ClaimsPrincipal identity, SendInviteRequest sendInviteRequest)
    {
        var senderId = identity.FindFirstValue(ClaimTypes.NameIdentifier);
        if (senderId is null)
        {
            throw new ServiceException("Not logged in.", StatusCodes.Status401Unauthorized);
        }

        var sender = await _context.Users.FindAsync(senderId);
        if (sender is null)
        {
            throw new ServiceException("User not found.", StatusCodes.Status404NotFound);
        }

        if (sender.Id == sendInviteRequest.UserId)
        {
            throw new ServiceException("Sender cannot be receiver.", StatusCodes.Status400BadRequest);
        }

        var receiver = await _context.Users.FindAsync(sendInviteRequest.UserId);
        if (receiver is null)
        {
            throw new ServiceException("User not found.", StatusCodes.Status404NotFound);
        }

        var organization = await _context.Organizations
            .Include(e => e.Users)
            .FirstOrDefaultAsync(e => e.Id == sendInviteRequest.OrganizationId);
        if (organization is null)
        {
            throw new ServiceException("Organization not found.", StatusCodes.Status404NotFound);
        }

        var invite = organization.Users
            .FirstOrDefault(e => e.Status == Status.Invited && e.UserId == sendInviteRequest.UserId);
        if (invite is not null)
        {
            throw new ServiceException("Invite already sent.", StatusCodes.Status409Conflict);
        }

        var organizationRole = await _context.OrganizationRoles.FindAsync(sendInviteRequest.OrganizationRoleId);
        if (organizationRole is null)
        {
            throw new ServiceException("Organization role not found.", StatusCodes.Status404NotFound);
        }

        var currentMembers = organization.Users
            .Where(e => e.Status == Status.User)
            .ToList();

        if (currentMembers.All(e => e.UserId != sender.Id))
        {
            throw new ServiceException("Unauthorized to send invites.", StatusCodes.Status403Forbidden);
        }

        if (currentMembers.Any(e => e.UserId == sendInviteRequest.UserId))
        {
            throw new ServiceException("User has already joined.", StatusCodes.Status400BadRequest);
        }

        var userOrganization = OrganizationMappings.ToEntity(
            Status.Invited,
            organization,
            receiver,
            organizationRole);

        organization.Users.Add(userOrganization);

        await _context.SaveChangesAsync();
    }
}