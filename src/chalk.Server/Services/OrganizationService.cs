using chalk.Server.Configurations;
using chalk.Server.Data;
using chalk.Server.DTOs.Requests;
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

    public async Task<IEnumerable<Organization>> GetOrganizationsAsync()
    {
        return await _context.Organizations
            .Include(e => e.Owner).AsSplitQuery()
            .Include(e => e.Users).ThenInclude(e => e.User).AsSplitQuery()
            .Include(e => e.Roles).AsSplitQuery()
            .Include(e => e.Courses).AsSplitQuery()
            .ToListAsync();
    }

    public async Task<Organization> GetOrganizationAsync(long organizationId)
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

        return organization;
    }

    public async Task<Organization> CreateOrganizationAsync(long userId, CreateOrganizationRequest request)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null)
        {
            throw new ServiceException("User not found.", StatusCodes.Status404NotFound);
        }

        if (await _context.Organizations.AnyAsync(e => e.Name == request.Name))
        {
            throw new ServiceException("Organization already exists.", StatusCodes.Status409Conflict);
        }

        var organization = request.ToEntity(user);
        var role = CreateRoleRequest
            .CreateOrganizationRole("Owner", null, PermissionUtilities.All, 0, organization.Id)
            .ToEntity();

        var userRole = new UserRole
        {
            UserId = user.Id,
            Role = role
        };
        var userOrganization = new UserOrganization
        {
            Status = UserStatus.Joined,
            JoinedDate = DateTime.UtcNow,
            User = user,
            Organization = organization,
        };
        userOrganization.Roles.Add(userRole);

        var createdOrganization = await _context.Organizations.AddAsync(organization);
        organization.Users.Add(userOrganization);
        organization.Roles.Add(role);

        await _context.SaveChangesAsync();
        return await GetOrganizationAsync(createdOrganization.Entity.Id);
    }

    public async Task<Organization> UpdateOrganizationAsync(long organizationId, UpdateOrganizationRequest request)
    {
        var organization = await _context.Organizations.FindAsync(organizationId);
        if (organization is null)
        {
            throw new ServiceException("Organization not found.", StatusCodes.Status404NotFound);
        }

        if (request.Name is not null)
        {
            if (await _context.Organizations.AnyAsync(e => e.Name == request.Name))
            {
                throw new ServiceException("Organization already exists.", StatusCodes.Status409Conflict);
            }

            organization.Name = request.Name;
        }

        if (request.Description is not null)
        {
            organization.Description = request.Description;
        }

        if (request.ProfilePicture is not null)
        {
            organization.ProfilePicture = request.ProfilePicture;
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
}