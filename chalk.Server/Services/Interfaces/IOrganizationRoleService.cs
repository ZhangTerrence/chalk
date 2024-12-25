using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;

namespace chalk.Server.Services.Interfaces;

public interface IOrganizationRoleService
{
    public Task<OrganizationRoleResponse> CreateOrganizationRoleAsync(
        CreateOrganizationRoleRequest createOrganizationRoleRequest);
}