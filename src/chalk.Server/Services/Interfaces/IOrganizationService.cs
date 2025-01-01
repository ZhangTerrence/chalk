using System.Security.Claims;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;

namespace chalk.Server.Services.Interfaces;

public interface IOrganizationService
{
    public Task<IEnumerable<OrganizationResponse>> GetOrganizationsAsync();

    public Task<OrganizationResponse> GetOrganizationAsync(long organizationId);

    public Task<OrganizationResponse> CreateOrganizationAsync(CreateOrganizationRequest createOrganizationRequest);

    public Task<OrganizationResponse> UpdateOrganizationAsync(
        long organizationId,
        UpdateOrganizationRequest updateOrganizationRequest);

    public Task DeleteOrganizationAsync(long organizationId);

    public Task SendInviteAsync(SendInviteRequest sendInviteRequest, ClaimsPrincipal authUser);
}