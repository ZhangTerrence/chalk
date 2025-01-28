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
[Route("/api/organizations"), Authorize]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    private readonly IValidator<CreateOrganizationRequest> _createOrganizationRequestValidator;
    private readonly IValidator<UpdateOrganizationRequest> _updateOrganizationRequestValidator;

    public OrganizationController(
        IOrganizationService organizationService,
        IValidator<CreateOrganizationRequest> createOrganizationRequestValidator,
        IValidator<UpdateOrganizationRequest> updateOrganizationRequestValidator
    )
    {
        _organizationService = organizationService;
        _createOrganizationRequestValidator = createOrganizationRequestValidator;
        _updateOrganizationRequestValidator = updateOrganizationRequestValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrganizations()
    {
        var organizations = await _organizationService.GetOrganizationsAsync();
        return Ok(new Response<IEnumerable<OrganizationResponse>>(null, organizations.Select(e => e.ToResponse())));
    }

    [HttpGet("{organizationId:long}")]
    public async Task<IActionResult> GetOrganization([FromRoute] long organizationId)
    {
        var organization = await _organizationService.GetOrganizationAsync(organizationId);
        return Ok(new Response<OrganizationResponse>(null, organization.ToResponse()));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationRequest request)
    {
        var validationResult = await _createOrganizationRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        var organization = await _organizationService.CreateOrganizationAsync(User.GetUserId(), request);
        return Created(nameof(CreateOrganization), new Response<OrganizationResponse>(null, organization.ToResponse()));
    }

    [HttpPut("{organizationId:long}")]
    public async Task<IActionResult> UpdateOrganization([FromRoute] long organizationId, [FromBody] UpdateOrganizationRequest request)
    {
        var validationResult = await _updateOrganizationRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        var organization = await _organizationService.UpdateOrganizationAsync(organizationId, request);
        return Ok(new Response<OrganizationResponse>(null, organization.ToResponse()));
    }

    [HttpDelete("{organizationId:long}")]
    public async Task<IActionResult> DeleteOrganization([FromRoute] long organizationId)
    {
        await _organizationService.DeleteOrganizationAsync(organizationId);
        return NoContent();
    }
}