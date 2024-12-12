using chalk.Server.DTOs;
using chalk.Server.DTOs.Responses;
using chalk.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Route("/api/organization")]
[Authorize(Roles = "User,Admin")]
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
        return Ok(new ApiResponseDTO<IEnumerable<OrganizationResponseDTO>>(null, organizations));
    }

    [HttpGet("{organizationId:long}")]
    public async Task<IActionResult> GetOrganization([FromRoute] long organizationId)
    {
        var organization = await _organizationService.GetOrganizationAsync(organizationId);
        return Ok(new ApiResponseDTO<OrganizationResponseDTO>(null, organization));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationDTO createOrganizationDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>(ModelState));
        }

        var createdOrganization = await _organizationService.CreateOrganizationAsync(createOrganizationDTO);
        return Ok(new ApiResponseDTO<OrganizationResponseDTO>(null, createdOrganization));
    }

    [HttpPatch("{organizationId:long}")]
    public async Task<IActionResult> UpdateOrganization(
        [FromRoute] long organizationId,
        [FromBody] UpdateOrganizationDTO updateOrganizationDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>(ModelState));
        }

        var updatedOrganization =
            await _organizationService.UpdateOrganizationAsync(organizationId, updateOrganizationDTO);
        return Ok(new ApiResponseDTO<OrganizationResponseDTO>(null, updatedOrganization));
    }

    [HttpDelete("{organizationId:long}")]
    public async Task<IActionResult> DeleteOrganization([FromRoute] long organizationId)
    {
        await _organizationService.DeleteOrganizationAsync(organizationId);
        return NoContent();
    }

    [HttpPost("invite")]
    public async Task<IActionResult> SendInvite([FromBody] SendInviteDTO sendInviteDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>(ModelState));
        }

        await _organizationService.SendInviteAsync(sendInviteDTO, User);
        return NoContent();
    }
}