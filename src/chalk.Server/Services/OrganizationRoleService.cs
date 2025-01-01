using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Services.Interfaces;

namespace chalk.Server.Services;

public class OrganizationRoleService : IOrganizationRoleService
{
    public async Task<OrganizationRoleResponse> CreateOrganizationRoleAsync(
        CreateOrganizationRoleRequest createOrganizationRoleRequest)
    {
        throw new NotImplementedException();
    }
}