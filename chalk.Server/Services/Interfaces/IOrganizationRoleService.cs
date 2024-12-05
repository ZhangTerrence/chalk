using chalk.Server.DTOs.OrganizationRole;

namespace chalk.Server.Services.Interfaces;

public interface IOrganizationRoleService
{
    public Task<OrganizationRoleDTO> CreateOrganizationRoleAsync(CreateOrganizationRoleDTO createOrganizationRoleDTO);
}