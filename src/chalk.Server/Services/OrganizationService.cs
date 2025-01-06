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
            .Select(e => e.ToDTO())
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

        return organization.ToDTO();
    }

    public async Task<OrganizationResponse> CreateOrganizationAsync(CreateOrganizationRequest request)
    {
        var user = await _context.Users.FindAsync(request.OwnerId);
        if (user is null)
        {
            throw new ServiceException("User not found.", StatusCodes.Status404NotFound);
        }

        if (await _context.Organizations.AnyAsync(e => e.Name == request.Name))
        {
            throw new ServiceException("Organization already exists.", StatusCodes.Status409Conflict);
        }

        var organization = request.ToEntity(user);

        var ownerRole = new OrganizationRole
        {
            Name = "Owner",
            Permissions = PermissionUtilities.All,
            Rank = 0,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };

        organization.Users.Add(new UserOrganization
        {
            UserStatus = UserStatus.Joined,
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
        UpdateOrganizationRequest request
    )
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

    public async Task<RoleResponse> CreateOrganizationRoleAsync(CreateOrganizationRoleRequest request)
    {
        throw new NotImplementedException();
    }
}