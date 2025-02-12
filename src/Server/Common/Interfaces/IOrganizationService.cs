using Server.Common.Requests.Organization;
using Server.Data.Entities;

namespace Server.Common.Interfaces;

public interface IOrganizationService
{
  public Task<IEnumerable<Organization>> GetOrganizationsAsync(long requesterId);

  public Task<Organization> GetOrganizationByIdAsync(long organizationId, long requesterId);

  public Task<Organization> CreateOrganizationAsync(long requesterId, CreateRequest request);

  public Task<Organization> UpdateOrganizationAsync(long organizationId, long requesterId, UpdateRequest request);

  public Task DeleteOrganizationAsync(long organizationId, long requesterId);
}
