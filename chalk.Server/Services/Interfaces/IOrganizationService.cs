using chalk.Server.DTOs.Organization;

namespace chalk.Server.Services.Interfaces;

public interface IOrganizationService
{
    public Task<IEnumerable<OrganizationResponseDTO>> GetOrganizationsAsync();

    public Task<OrganizationResponseDTO> GetOrganizationAsync(long organizationId);

    public Task<OrganizationResponseDTO> CreateOrganizationAsync(CreateOrganizationDTO createOrganizationDTO);

    public Task<OrganizationResponseDTO> UpdateOrganizationAsync(
        long organizationId,
        UpdateOrganizationDTO updateOrganizationDTO);

    public Task DeleteOrganizationAsync(long organizationId);

    public Task<UserOrganizationDTO> SendInviteAsync(long senderId, SendInviteDTO sendInviteDTO);
}