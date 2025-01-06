using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;

namespace chalk.Server.Services.Interfaces;

public interface IOrganizationService
{
    public Task<IEnumerable<OrganizationResponse>> GetOrganizationsAsync();

    public Task<OrganizationResponse> GetOrganizationAsync(long organizationId);

    public Task<OrganizationResponse> CreateOrganizationAsync(CreateOrganizationRequest request);

    public Task<OrganizationResponse> UpdateOrganizationAsync(long organizationId, UpdateOrganizationRequest request);

    public Task DeleteOrganizationAsync(long organizationId);

    public Task<RoleResponse> CreateOrganizationRoleAsync(CreateRoleRequest request);
}