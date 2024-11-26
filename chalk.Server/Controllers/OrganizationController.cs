using chalk.Server.DTOs;
using chalk.Server.DTOs.Organization;
using chalk.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public OrganizationController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpGet]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> GetOrganizations()
    {
        var organizations = await _organizationService.GetOrganizationsAsync();
        return Ok(new ApiResponseDTO<IEnumerable<OrganizationDTO>>(organizations));
    }

    [HttpGet("{organizationId:guid}")]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> GetOrganization([FromRoute] Guid organizationId)
    {
        var organization = await _organizationService.GetOrganizationAsync(organizationId);
        return Ok(new ApiResponseDTO<OrganizationDTO>(organization));
    }

    [HttpPost]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationDTO createOrganizationDTO)
    {
        var createdOrganization = await _organizationService.CreateOrganizationAsync(createOrganizationDTO);
        return Ok(new ApiResponseDTO<OrganizationDTO>(createdOrganization));
    }

    [HttpPatch("{organizationId:guid}")]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> UpdateOrganization(
        [FromRoute] Guid organizationId,
        [FromBody] UpdateOrganizationDTO updateOrganizationDTO)
    {
        var updatedOrganization =
            await _organizationService.UpdateOrganizationAsync(organizationId, updateOrganizationDTO);
        return Ok(new ApiResponseDTO<OrganizationDTO>(updatedOrganization));
    }

    [HttpDelete("{organizationId:guid}")]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> DeleteOrganization([FromRoute] Guid organizationId)
    {
        await _organizationService.DeleteOrganizationAsync(organizationId);
        return NoContent();
    }
}