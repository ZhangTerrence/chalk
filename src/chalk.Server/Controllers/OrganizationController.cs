using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
using chalk.Server.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Route("/api/organization"), Authorize]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    private readonly IValidator<CreateOrganizationRequest> _createOrganizationValidator;
    private readonly IValidator<UpdateOrganizationRequest> _updateOrganizationValidator;
    private readonly IValidator<CreateRoleRequest> _createRoleValidator;

    public OrganizationController(
        IOrganizationService organizationService,
        IValidator<CreateOrganizationRequest> createOrganizationValidator,
        IValidator<UpdateOrganizationRequest> updateOrganizationValidator,
        IValidator<CreateRoleRequest> createRoleValidator
    )
    {
        _organizationService = organizationService;
        _createOrganizationValidator = createOrganizationValidator;
        _updateOrganizationValidator = updateOrganizationValidator;
        _createRoleValidator = createRoleValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrganizations()
    {
        var organizations = await _organizationService.GetOrganizationsAsync();
        return Ok(new ApiResponse<IEnumerable<OrganizationResponse>>(null, organizations.Select(e => e.ToResponse())));
    }

    [HttpGet("{organizationId:long}")]
    public async Task<IActionResult> GetOrganization([FromRoute] long organizationId)
    {
        var organization = await _organizationService.GetOrganizationAsync(organizationId);
        return Ok(new ApiResponse<OrganizationResponse>(null, organization.ToResponse()));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationRequest request)
    {
        var result = await _createOrganizationValidator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return BadRequest(new ApiResponse<object>(result.GetErrorMessages()));
        }

        var organization = await _organizationService.CreateOrganizationAsync(User.GetUserId(), request);
        return Created(nameof(CreateOrganization), new ApiResponse<OrganizationResponse>(null, organization.ToResponse()));
    }

    [HttpPatch("{organizationId:long}")]
    public async Task<IActionResult> UpdateOrganization([FromRoute] long organizationId, [FromBody] UpdateOrganizationRequest request)
    {
        var result = await _updateOrganizationValidator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return BadRequest(new ApiResponse<object>(result.GetErrorMessages()));
        }

        var organization = await _organizationService.UpdateOrganizationAsync(organizationId, request);
        return Ok(new ApiResponse<OrganizationResponse>(null, organization.ToResponse()));
    }

    [HttpDelete("{organizationId:long}")]
    public async Task<IActionResult> DeleteOrganization([FromRoute] long organizationId)
    {
        await _organizationService.DeleteOrganizationAsync(organizationId);
        return NoContent();
    }

    [HttpPost("role")]
    public async Task<IActionResult> CreateOrganizationRole([FromBody] CreateRoleRequest request)
    {
        var result = await _createRoleValidator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return BadRequest(new ApiResponse<object>(result.GetErrorMessages()));
        }

        var organizationRole = await _organizationService.CreateOrganizationRoleAsync(request);
        return Created(nameof(CreateOrganizationRole), new ApiResponse<RoleResponse>(null, organizationRole.ToResponse()));
    }
}