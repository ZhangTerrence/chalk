using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.Organization;
using Server.Common.Responses;
using Server.Common.Utilities;

namespace Server.Controllers;

[ApiController]
[Authorize]
[Route("/api/organizations")]
public class OrganizationController : ControllerBase
{
  private readonly IOrganizationService _organizationService;

  public OrganizationController(IOrganizationService organizationService)
  {
    this._organizationService = organizationService;
  }

  [HttpGet]
  public async Task<IActionResult> GetOrganizations()
  {
    var organizations = await this._organizationService.GetOrganizationsAsync(this.User.GetUserId());
    return this.Ok(new Response<IEnumerable<OrganizationResponse>>(null, organizations.Select(e => e.ToResponse())));
  }

  [HttpGet("{organizationId:long}")]
  public async Task<IActionResult> GetOrganization([FromRoute] long organizationId)
  {
    var organization = await this._organizationService.GetOrganizationByIdAsync(organizationId, this.User.GetUserId());
    return this.Ok(new Response<OrganizationResponse>(null, organization.ToResponse()));
  }

  [HttpPost]
  public async Task<IActionResult> CreateOrganization([FromBody] CreateRequest request)
  {
    var organization = await this._organizationService.CreateOrganizationAsync(this.User.GetUserId(), request);
    return this.Created(nameof(this.CreateOrganization),
      new Response<OrganizationResponse>(null, organization.ToResponse()));
  }

  [HttpPut("{organizationId:long}")]
  public async Task<IActionResult> UpdateOrganization([FromRoute] long organizationId, [FromBody] UpdateRequest request)
  {
    var organization =
      await this._organizationService.UpdateOrganizationAsync(organizationId, this.User.GetUserId(), request);
    return this.Ok(new Response<OrganizationResponse>(null, organization.ToResponse()));
  }

  [HttpDelete("{organizationId:long}")]
  public async Task<IActionResult> DeleteOrganization([FromRoute] long organizationId)
  {
    await this._organizationService.DeleteOrganizationAsync(organizationId, this.User.GetUserId());
    return this.NoContent();
  }
}
