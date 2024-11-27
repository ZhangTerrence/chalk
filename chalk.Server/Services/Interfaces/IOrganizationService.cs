using chalk.Server.DTOs.Organization;

namespace chalk.Server.Services.Interfaces;

public interface IOrganizationService
{
    public Task<IEnumerable<OrganizationDTO>> GetOrganizationsAsync();

    public Task<OrganizationDTO> GetOrganizationAsync(long organizationId);

    public Task<OrganizationDTO> CreateOrganizationAsync(CreateOrganizationDTO createOrganizationDTO);

    public Task<OrganizationDTO> UpdateOrganizationAsync(
        long organizationId,
        UpdateOrganizationDTO updateOrganizationDTO);

    public Task DeleteOrganizationAsync(long organizationId);
}