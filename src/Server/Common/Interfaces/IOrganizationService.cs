using Server.Common.Requests.Organization;
using Server.Data.Entities;

namespace Server.Common.Interfaces;

/// <summary>
/// Interface for organization services.
/// </summary>
public interface IOrganizationService
{
  /// <summary>
  /// Gets all organizations.
  /// </summary>
  /// <param name="requesterId">The requester's id.</param>
  /// <returns>A list of the organizations.</returns>
  public Task<IEnumerable<Organization>> GetOrganizationsAsync(long requesterId);

  /// <summary>
  /// Gets an organization by id.
  /// </summary>
  /// <param name="organizationId">The organization's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  /// <returns>The organization.</returns>
  public Task<Organization> GetOrganizationByIdAsync(long organizationId, long requesterId);

  /// <summary>
  /// Creates an organization.
  /// </summary>
  /// <param name="requesterId">The requester's id.</param>
  /// <param name="request">The request body. See <see cref="CreateRequest" /> for more details.</param>
  /// <returns>The created organization.</returns>
  public Task<Organization> CreateOrganizationAsync(long requesterId, CreateRequest request);

  /// <summary>
  /// Updates an organization.
  /// </summary>
  /// <param name="organizationId">The organization's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  /// <param name="request">The request body. See <see cref="UpdateRequest" /> for more details.</param>
  /// <returns>The updated organization.</returns>
  public Task<Organization> UpdateOrganizationAsync(long organizationId, long requesterId, UpdateRequest request);

  /// <summary>
  /// Deletes an organization.
  /// </summary>
  /// <param name="organizationId">The organization's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  public Task DeleteOrganizationAsync(long organizationId, long requesterId);
}
