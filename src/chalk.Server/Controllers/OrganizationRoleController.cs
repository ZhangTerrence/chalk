using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Route("/api/organization-role")]
[Authorize]
public class OrganizationRoleController : ControllerBase
{
    // Validators
    private readonly IValidator<CreateOrganizationRoleRequest> _createOrganizationRoleValidator;

    public OrganizationRoleController(IValidator<CreateOrganizationRoleRequest> createOrganizationRoleValidator)
    {
        _createOrganizationRoleValidator = createOrganizationRoleValidator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateOrganizationRoleRequest createOrganizationRoleRequest)
    {
        var result = await _createOrganizationRoleValidator.ValidateAsync(createOrganizationRoleRequest);
        if (!result.IsValid)
        {
            return BadRequest(new ApiResponse<object>(result.Errors.Select(e => e.ErrorMessage), null));
        }

        return Ok();
    }
}