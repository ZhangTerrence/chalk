using chalk.Server.DTOs.Organization;

namespace chalk.Server.Services.Interfaces;

public interface IOrganizationService
{
    public Task<IEnumerable<OrganizationDTO>> GetOrganizationsAsync();

    public Task<OrganizationDTO> GetOrganizationAsync(Guid organizationId);

    public Task<OrganizationDTO> CreateOrganizationAsync(CreateOrganizationDTO createOrganizationDTO);

    public Task<OrganizationDTO> UpdateOrganizationAsync(
        Guid organizationId,
        UpdateOrganizationDTO updateOrganizationDTO);

    public Task DeleteOrganizationAsync(Guid organizationId);
}