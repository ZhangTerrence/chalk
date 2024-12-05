using chalk.Server.DTOs;
using chalk.Server.DTOs.OrganizationRole;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Authorize("User,Admin")]
[Route("/api/organization-role")]
public class OrganizationRoleController : ControllerBase
{
    public OrganizationRoleController()
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateOrganizationRoleDTO createOrganizationRoleDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>(ModelState));
        }

        return Ok();
    }
}