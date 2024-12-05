using System.Security.Claims;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Organization;
using chalk.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Authorize(Roles = "User,Admin")]
[Route("/api/[controller]")]
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
        return Ok(new ApiResponseDTO<IEnumerable<OrganizationDTO>>(organizations));
    }

    [HttpGet("{organizationId:long}")]
    public async Task<IActionResult> GetOrganization([FromRoute] long organizationId)
    {
        var organization = await _organizationService.GetOrganizationAsync(organizationId);
        return Ok(new ApiResponseDTO<OrganizationDTO>(organization));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationDTO createOrganizationDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>(ModelState));
        }

        var createdOrganization = await _organizationService.CreateOrganizationAsync(createOrganizationDTO);
        return Ok(new ApiResponseDTO<OrganizationDTO>(createdOrganization));
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
        return Ok(new ApiResponseDTO<OrganizationDTO>(updatedOrganization));
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

        var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)!.Value;
        var invite = await _organizationService.SendInviteAsync(long.Parse(userId), sendInviteDTO);
        return Ok(new ApiResponseDTO<UserOrganizationDTO>(invite));
    }
}