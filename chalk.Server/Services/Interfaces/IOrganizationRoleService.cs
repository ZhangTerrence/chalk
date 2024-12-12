using chalk.Server.DTOs;
using chalk.Server.DTOs.Responses;

namespace chalk.Server.Services.Interfaces;

public interface IOrganizationRoleService
{
    public Task<OrganizationRoleResponseDTO> CreateOrganizationRoleAsync(
        CreateOrganizationRoleDTO createOrganizationRoleDTO);
}