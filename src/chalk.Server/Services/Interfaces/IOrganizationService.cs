using chalk.Server.DTOs.Requests;
using chalk.Server.Entities;

namespace chalk.Server.Services.Interfaces;

public interface IOrganizationService
{
    public Task<IEnumerable<Organization>> GetOrganizationsAsync();

    public Task<Organization> GetOrganizationAsync(long organizationId);

    public Task<Organization> CreateOrganizationAsync(long userId, CreateOrganizationRequest request);

    public Task<Organization> UpdateOrganizationAsync(long organizationId, UpdateOrganizationRequest request);

    public Task DeleteOrganizationAsync(long organizationId);

    public Task<OrganizationRole> CreateOrganizationRoleAsync(CreateRoleRequest request);
}