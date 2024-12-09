using chalk.Server.Common;
using chalk.Server.Data;
using chalk.Server.DTOs.Organization;
using chalk.Server.Entities;
using chalk.Server.Extensions;
using chalk.Server.Services.Interfaces;
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

    public async Task<IEnumerable<OrganizationResponseDTO>> GetOrganizationsAsync()
    {
        return await _context.Organizations
            .Include(e => e.Owner)
            .Include(e => e.UserOrganizations).ThenInclude(e => e.User)
            .Include(e => e.OrganizationRoles)
            .Include(e => e.Courses)
            .Select(e => e.ToOrganizationDTO())
            .ToListAsync();
    }


    public async Task<OrganizationResponseDTO> GetOrganizationAsync(long organizationId)
    {
        var organization = await _context.Organizations
            .Include(e => e.Owner)
            .Include(e => e.UserOrganizations).ThenInclude(e => e.User)
            .Include(e => e.OrganizationRoles)
            .Include(e => e.Courses)
            .FirstOrDefaultAsync(e => e.Id == organizationId);
        if (organization is null)
        {
            throw new BadHttpRequestException("Organization not found.", StatusCodes.Status404NotFound);
        }

        return organization.ToOrganizationDTO();
    }

    public async Task<OrganizationResponseDTO> CreateOrganizationAsync(CreateOrganizationDTO createOrganizationDTO)
    {
        var user = await _context.Users.FindAsync(createOrganizationDTO.OwnerId);
        if (user is null)
        {
            throw new BadHttpRequestException("User not found.", StatusCodes.Status404NotFound);
        }

        var organizationExists = await _context.Organizations.AnyAsync(e => e.Name == createOrganizationDTO.Name);
        if (organizationExists)
        {
            throw new BadHttpRequestException("Organization name taken.", StatusCodes.Status400BadRequest);
        }

        var organization = createOrganizationDTO.ToOrganization(user);

        var userRole = new OrganizationRole
        {
            Name = nameof(Role.User),
            Permissions = OrganizationPermissionsUtilities.BaseUserPermissions(),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
        var adminRole = new OrganizationRole
        {
            Name = nameof(Role.Admin),
            Permissions = OrganizationPermissionsUtilities.BaseAdminPermissions(),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };

        organization.UserOrganizations.Add(new UserOrganization
        {
            Status = MemberStatus.User,
            JoinedDate = DateTime.UtcNow,
            User = user,
            Organization = organization,
            OrganizationRole = adminRole
        });

        organization.OrganizationRoles.Add(userRole);
        organization.OrganizationRoles.Add(adminRole);

        var createdOrganization = await _context.Organizations.AddAsync(organization);

        await _context.SaveChangesAsync();
        return await GetOrganizationAsync(createdOrganization.Entity.Id);
    }

    public async Task<OrganizationResponseDTO> UpdateOrganizationAsync(
        long organizationId,
        UpdateOrganizationDTO updateOrganizationDTO)
    {
        var organization = await _context.Organizations.FindAsync(organizationId);
        if (organization is null)
        {
            throw new BadHttpRequestException("Organization not found.", StatusCodes.Status404NotFound);
        }

        if (updateOrganizationDTO.Name is not null)
        {
            var organizationExists = await _context.Organizations.AnyAsync(e => e.Name == updateOrganizationDTO.Name);
            if (organizationExists)
            {
                throw new BadHttpRequestException("Organization name already taken.", StatusCodes.Status409Conflict);
            }

            organization.Name = updateOrganizationDTO.Name;
        }

        if (updateOrganizationDTO.Description is not null)
        {
            organization.Description = updateOrganizationDTO.Description;
        }

        if (updateOrganizationDTO.OwnerId is not null)
        {
            var user = await _context.Users.FindAsync(updateOrganizationDTO.OwnerId);
            if (user is null)
            {
                throw new BadHttpRequestException("User not found.", StatusCodes.Status404NotFound);
            }

            organization.Owner = user;
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
            throw new BadHttpRequestException("Organization not found.", StatusCodes.Status404NotFound);
        }

        _context.Organizations.Remove(organization);

        await _context.SaveChangesAsync();
    }

    public async Task<UserOrganizationDTO> SendInviteAsync(long senderId, SendInviteDTO sendInviteDTO)
    {
        if (senderId == sendInviteDTO.UserId)
        {
            throw new BadHttpRequestException("Can't invite yourself.", StatusCodes.Status400BadRequest);
        }

        var user = await _context.Users.FindAsync(sendInviteDTO.UserId);
        if (user is null)
        {
            throw new BadHttpRequestException("User not found.", StatusCodes.Status404NotFound);
        }

        var organization = await _context.Organizations
            .Include(e => e.UserOrganizations)
            .FirstOrDefaultAsync(e => e.Id == sendInviteDTO.OrganizationId);
        if (organization is null)
        {
            throw new BadHttpRequestException("Organization not found.", StatusCodes.Status404NotFound);
        }

        var invite = organization.UserOrganizations
            .FirstOrDefault(e => e.Status == MemberStatus.Invited && e.UserId == sendInviteDTO.UserId);
        if (invite is not null)
        {
            throw new BadHttpRequestException("Invite already sent.", StatusCodes.Status409Conflict);
        }

        var organizationRole = await _context.OrganizationRoles.FindAsync(sendInviteDTO.OrganizationRoleId);
        if (organizationRole is null)
        {
            throw new BadHttpRequestException("Organization role not found.", StatusCodes.Status404NotFound);
        }

        var currentMembers = organization.UserOrganizations
            .Where(e => e.Status == MemberStatus.User)
            .ToList();

        if (currentMembers.All(e => e.UserId != senderId))
        {
            throw new BadHttpRequestException("Unauthorized to invite.", StatusCodes.Status403Forbidden);
        }

        if (currentMembers.Any(e => e.UserId == sendInviteDTO.UserId))
        {
            throw new BadHttpRequestException("User is already a member.", StatusCodes.Status400BadRequest);
        }

        var userOrganization = OrganizationExtensions.ToUserOrganization(
            MemberStatus.Invited,
            organization,
            user,
            organizationRole);

        organization.UserOrganizations.Add(userOrganization);

        await _context.SaveChangesAsync();

        return userOrganization.ToUserOrganizationDTO();
    }
}