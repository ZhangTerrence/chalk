using chalk.Server.Data;
using chalk.Server.DTOs.Organization;
using chalk.Server.Extensions;
using chalk.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace chalk.Server.Services;

public class OrganizationService : IOrganizationService
{
    private readonly DatabaseContext _context;

    public OrganizationService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrganizationDTO>> GetOrganizationsAsync()
    {
        return await _context.Organizations
            .Include(e => e.Courses)
            .Select(e => e.ToOrganizationResponseDTO())
            .ToListAsync();
    }

    public async Task<OrganizationDTO> GetOrganizationAsync(long organizationId)
    {
        var organization = await _context.Organizations
            .Include(e => e.Courses)
            .FirstOrDefaultAsync(e => e.Id == organizationId);
        if (organization is null)
        {
            throw new BadHttpRequestException("Organization not found.", StatusCodes.Status404NotFound);
        }

        return organization.ToOrganizationResponseDTO();
    }

    public async Task<OrganizationDTO> CreateOrganizationAsync(CreateOrganizationDTO createOrganizationDTO)
    {
        var user = await _context.Users.FindAsync(createOrganizationDTO.OwnerId);
        if (user is null)
        {
            throw new BadHttpRequestException("User not found.", StatusCodes.Status404NotFound);
        }

        var organization = createOrganizationDTO.ToOrganization(user);
        var createdOrganization = await _context.Organizations.AddAsync(organization);

        await _context.SaveChangesAsync();

        return createdOrganization.Entity.ToOrganizationResponseDTO();
    }

    public async Task<OrganizationDTO> UpdateOrganizationAsync(
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

        await _context.SaveChangesAsync();
        return organization.ToOrganizationResponseDTO();
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
}