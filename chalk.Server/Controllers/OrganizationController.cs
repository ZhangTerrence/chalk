using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Route("/api/organization")]
[Authorize]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public OrganizationController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrganizations()
    {
        var organizations = await _organizationService.GetOrganizationsAsync();
        return Ok(new ApiResponse<IEnumerable<OrganizationResponse>>(null, organizations));
    }

    [HttpGet("{organizationId:long}")]
    public async Task<IActionResult> GetOrganization([FromRoute] long organizationId)
    {
        var organization = await _organizationService.GetOrganizationAsync(organizationId);
        return Ok(new ApiResponse<OrganizationResponse>(null, organization));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationRequest createOrganizationRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponse<object>(ModelState));
        }

        var createdOrganization = await _organizationService.CreateOrganizationAsync(createOrganizationRequest);
        return Ok(new ApiResponse<OrganizationResponse>(null, createdOrganization));
    }

    [HttpPatch("{organizationId:long}")]
    public async Task<IActionResult> UpdateOrganization(
        [FromRoute] long organizationId,
        [FromBody] UpdateOrganizationRequest updateOrganizationRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponse<object>(ModelState));
        }

        var updatedOrganization =
            await _organizationService.UpdateOrganizationAsync(organizationId, updateOrganizationRequest);
        return Ok(new ApiResponse<OrganizationResponse>(null, updatedOrganization));
    }

    [HttpDelete("{organizationId:long}")]
    public async Task<IActionResult> DeleteOrganization([FromRoute] long organizationId)
    {
        await _organizationService.DeleteOrganizationAsync(organizationId);
        return NoContent();
    }

    [HttpPost("invite")]
    public async Task<IActionResult> SendInvite([FromBody] SendInviteRequest sendInviteRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponse<object>(ModelState));
        }

        await _organizationService.SendInviteAsync(sendInviteRequest, User);
        return NoContent();
    }
}