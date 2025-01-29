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
    private readonly ICloudService _cloudService;

    public OrganizationService(DatabaseContext context, ICloudService cloudService)
    {
        _context = context;
        _cloudService = cloudService;
    }

    public async Task<IEnumerable<Organization>> GetOrganizationsAsync()
    {
        return await _context.Organizations
            .ToListAsync();
    }

    public async Task<Organization> GetOrganizationAsync(long organizationId)
    {
        var organization = await _context.Organizations
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
        var createdOrganization = await _context.Organizations.AddAsync(organization);
        var role = new CreateRoleRequest("Owner", null, PermissionUtilities.All, 0)
            .ToEntity(null, organization.Id);
        var userOrganization = new UserOrganization
        {
            Status = UserStatus.Joined,
            JoinedDate = DateTime.UtcNow,
            User = user,
            Organization = organization,
        };
        var userRole = new UserRole
        {
            UserOrganization = userOrganization,
            Role = role
        };
        userOrganization.Roles.Add(userRole);
        organization.Users.Add(userOrganization);
        organization.Roles.Add(role);

        await _context.SaveChangesAsync();
        return await GetOrganizationAsync(createdOrganization.Entity.Id);
    }

    public async Task<Organization> UpdateOrganizationAsync(long organizationId, UpdateOrganizationRequest request)
    {
        if (await _context.Organizations.AnyAsync(e => e.Name == request.Name))
        {
            throw new ServiceException("Organization already exists.", StatusCodes.Status409Conflict);
        }

        var organization = await GetOrganizationAsync(organizationId);
        organization.Name = request.Name;
        organization.Description = request.Description;
        if (request.Image is not null)
        {
            organization.ImageUrl = await _cloudService.UploadImageAsync(Guid.NewGuid().ToString(), request.Image);
        }
        organization.IsPublic = request.IsPublic!.Value;
        organization.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return await GetOrganizationAsync(organizationId);
    }

    public async Task DeleteOrganizationAsync(long organizationId)
    {
        var organization = await GetOrganizationAsync(organizationId);
        _context.Organizations.Remove(organization);

        await _context.SaveChangesAsync();
    }
}