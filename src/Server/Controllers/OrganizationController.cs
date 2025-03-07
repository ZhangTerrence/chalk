using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.Organization;
using Server.Common.Responses;
using Server.Common.Utilities;

namespace Server.Controllers;

/// <summary>
/// Routes for managing organizations.
/// </summary>
[ApiController] [Authorize]
[Route("/api/organizations")]
[Produces("application/json")]
public class OrganizationController : ControllerBase
{
  private readonly IOrganizationService _organizationService;

  internal OrganizationController(IOrganizationService organizationService)
  {
    this._organizationService = organizationService;
  }

  /// <summary>
  /// Gets all organizations.
  /// </summary>
  /// <returns>A list of all the organizations.</returns>
  [HttpGet]
  [ProducesResponseType<Response<IEnumerable<OrganizationResponse>>>(StatusCodes.Status200OK)]
  public async Task<IActionResult> GetOrganizations()
  {
    var organizations = await this._organizationService.GetOrganizationsAsync(this.User.GetUserId());
    return this.Ok(new Response<IEnumerable<OrganizationResponse>>(null, organizations.Select(e => e.ToResponse())));
  }

  /// <summary>
  /// Gets an organization.
  /// </summary>
  /// <param name="organizationId">The organization's id.</param>
  /// <returns>The organization.</returns>
  [HttpGet("{organizationId:long}")]
  [ProducesResponseType<Response<OrganizationResponse>>(StatusCodes.Status200OK)]
  public async Task<IActionResult> GetOrganization([FromRoute] long organizationId)
  {
    var organization = await this._organizationService.GetOrganizationByIdAsync(organizationId, this.User.GetUserId());
    return this.Ok(new Response<OrganizationResponse>(null, organization.ToResponse()));
  }

  /// <summary>
  /// Creates an organization.
  /// </summary>
  /// <param name="request">The request body. See <see cref="CreateRequest" /> for more details.</param>
  /// <returns>The created organization.</returns>
  [HttpPost]
  [ProducesResponseType<Response<OrganizationResponse>>(StatusCodes.Status201Created)]
  public async Task<IActionResult> CreateOrganization([FromBody] CreateRequest request)
  {
    var organization = await this._organizationService.CreateOrganizationAsync(this.User.GetUserId(), request);
    return this.Created(nameof(this.CreateOrganization),
      new Response<OrganizationResponse>(null, organization.ToResponse()));
  }

  /// <summary>
  /// Updates an organization.
  /// </summary>
  /// <param name="organizationId">The organization's id.</param>
  /// <param name="request">The request body. See <see cref="UpdateRequest" /> for more details.</param>
  /// <returns>The updated organization.</returns>
  [HttpPut("{organizationId:long}")]
  [ProducesResponseType<Response<OrganizationResponse>>(StatusCodes.Status200OK)]
  public async Task<IActionResult> UpdateOrganization([FromRoute] long organizationId, [FromBody] UpdateRequest request)
  {
    var organization =
      await this._organizationService.UpdateOrganizationAsync(organizationId, this.User.GetUserId(), request);
    return this.Ok(new Response<OrganizationResponse>(null, organization.ToResponse()));
  }

  /// <summary>
  /// Deletes an organization.
  /// </summary>
  /// <param name="organizationId">The organization's id.</param>
  [HttpDelete("{organizationId:long}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  public async Task<IActionResult> DeleteOrganization([FromRoute] long organizationId)
  {
    await this._organizationService.DeleteOrganizationAsync(organizationId, this.User.GetUserId());
    return this.NoContent();
  }
}
