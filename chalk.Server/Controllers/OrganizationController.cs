using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Services.Interfaces;
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
    private readonly IValidator<SendInviteRequest> _sendInviteValidator;

    public OrganizationController(
        IOrganizationService organizationService,
        IValidator<CreateOrganizationRequest> createOrganizationValidator,
        IValidator<UpdateOrganizationRequest> updateOrganizationValidator,
        IValidator<SendInviteRequest> sendInviteValidator
    )
    {
        _organizationService = organizationService;

        _createOrganizationValidator = createOrganizationValidator;
        _updateOrganizationValidator = updateOrganizationValidator;
        _sendInviteValidator = sendInviteValidator;
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
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationRequest createOrganizationRequest)
    {
        var result = await _createOrganizationValidator.ValidateAsync(createOrganizationRequest);
        if (!result.IsValid)
        {
            return BadRequest(new ApiResponse<object>(result.Errors.Select(e => e.ErrorMessage), null));
        }

        var createdOrganization = await _organizationService.CreateOrganizationAsync(createOrganizationRequest);
        return Ok(new ApiResponse<OrganizationResponse>(null, createdOrganization));
    }

    [HttpPatch("{id:long}")]
    public async Task<IActionResult> UpdateOrganization(
        [FromRoute] long id,
        [FromBody] UpdateOrganizationRequest updateOrganizationRequest
    )
    {
        var result = await _updateOrganizationValidator.ValidateAsync(updateOrganizationRequest);
        if (!result.IsValid)
        {
            return BadRequest(new ApiResponse<object>(result.Errors.Select(e => e.ErrorMessage), null));
        }

        var updatedOrganization = await _organizationService.UpdateOrganizationAsync(id, updateOrganizationRequest);
        return Ok(new ApiResponse<OrganizationResponse>(null, updatedOrganization));
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteOrganization([FromRoute] long id)
    {
        await _organizationService.DeleteOrganizationAsync(id);
        return NoContent();
    }

    [HttpPost("invite")]
    public async Task<IActionResult> SendInvite([FromBody] SendInviteRequest sendInviteRequest)
    {
        var result = await _sendInviteValidator.ValidateAsync(sendInviteRequest);
        if (!result.IsValid)
        {
            return BadRequest(new ApiResponse<object>(result.Errors.Select(e => e.ErrorMessage), null));
        }

        await _organizationService.SendInviteAsync(sendInviteRequest, User);
        return NoContent();
    }
}