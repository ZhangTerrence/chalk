using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Services.Interfaces;
using chalk.Server.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Route("/api/organization")]
[Authorize]
public class OrganizationController : ControllerBase
{
    // Services
    private readonly IOrganizationService _organizationService;

    // Validators
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
        return Ok(new ApiResponse<IEnumerable<OrganizationResponse>>(null, organizations));
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetOrganization([FromRoute] long id)
    {
        var organization = await _organizationService.GetOrganizationAsync(id);
        return Ok(new ApiResponse<OrganizationResponse>(null, organization));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationRequest request)
    {
        var result = await _createOrganizationValidator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return BadRequest(new ApiResponse<object>(result.GetErrorMessages()));
        }

        var createdOrganization = await _organizationService.CreateOrganizationAsync(request);
        return Ok(new ApiResponse<OrganizationResponse>(null, createdOrganization));
    }

    [HttpPatch("{id:long}")]
    public async Task<IActionResult> UpdateOrganization(
        [FromRoute] long id,
        [FromBody] UpdateOrganizationRequest request
    )
    {
        var result = await _updateOrganizationValidator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return BadRequest(new ApiResponse<object>(result.GetErrorMessages()));
        }

        var updatedOrganization = await _organizationService.UpdateOrganizationAsync(id, request);
        return Ok(new ApiResponse<OrganizationResponse>(null, updatedOrganization));
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteOrganization([FromRoute] long id)
    {
        await _organizationService.DeleteOrganizationAsync(id);
        return NoContent();
    }

    [HttpPost("role")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
    {
        var result = await _createRoleValidator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return BadRequest(new ApiResponse<object>(result.GetErrorMessages()));
        }

        return Ok();
    }
}