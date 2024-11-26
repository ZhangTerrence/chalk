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
            .Include(e => e.Owner)
            .Select(e => e.ToOrganizationResponseDTO())
            .ToListAsync();
    }

    public async Task<OrganizationDTO> GetOrganizationAsync(Guid organizationId)
    {
        var organization = await _context.Organizations
            .Include(e => e.Owner)
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
        await _context.Organizations.AddAsync(organization);

        await _context.SaveChangesAsync();
        return organization.ToOrganizationResponseDTO();
    }

    public async Task<OrganizationDTO> UpdateOrganizationAsync(
        Guid organizationId,
        UpdateOrganizationDTO updateOrganizationDTO)
    {
        var organization = _context.Organizations
            .Include(e => e.Owner)
            .FirstOrDefault(e => e.Id == organizationId);
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

        await _context.SaveChangesAsync();
        return organization.ToOrganizationResponseDTO();
    }

    public async Task DeleteOrganizationAsync(Guid organizationId)
    {
        var organization = _context.Organizations
            .Include(e => e.Owner)
            .FirstOrDefault(e => e.Id == organizationId);
        if (organization is null)
        {
            throw new BadHttpRequestException("Organization not found.", StatusCodes.Status404NotFound);
        }

        _context.Organizations.Remove(organization);

        await _context.SaveChangesAsync();
    }
}