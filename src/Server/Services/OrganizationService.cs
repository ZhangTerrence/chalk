using Microsoft.EntityFrameworkCore;
using Server.Common.Enums;
using Server.Common.Exceptions;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.Organization;
using Server.Common.Utilities;
using Server.Data;
using Server.Data.Entities;
using CreateRoleRequest = Server.Common.Requests.Role.CreateRequest;

namespace Server.Services;

internal class OrganizationService : IOrganizationService
{
  private readonly ICloudService _cloudService;
  private readonly DatabaseContext _context;

  public OrganizationService(DatabaseContext context, ICloudService cloudService)
  {
    this._context = context;
    this._cloudService = cloudService;
  }

  public async Task<IEnumerable<Organization>> GetOrganizationsAsync(long requesterId)
  {
    return await this._context.Organizations
      .ToListAsync();
  }

  public async Task<Organization> GetOrganizationByIdAsync(long organizationId, long requesterId)
  {
    var organization = await this._context.Organizations
      .FirstOrDefaultAsync(e => e.Id == organizationId);
    if (organization is null) ServiceException.NotFound("Organization not found.", organization);

    return organization;
  }

  public async Task<Organization> CreateOrganizationAsync(long requesterId, CreateRequest request)
  {
    var user = await this._context.Users.FindAsync(requesterId);
    if (user is null) ServiceException.NotFound("User not found.", user);

    var organization = request.ToEntity(user.Id);
    if (await this._context.Organizations.AnyAsync(e => e.Name == request.Name))
      throw new ServiceException(StatusCodes.Status409Conflict, ["Organization already exists."]);

    var createdOrganization = await this._context.Organizations.AddAsync(organization);
    var role = new CreateRoleRequest("Owner", null, PermissionUtilities.All, 0)
      .ToEntity(null, organization.Id);
    var userOrganization = new UserOrganization
    {
      Status = UserStatus.Joined,
      JoinedOnUtc = DateTime.UtcNow,
      User = user,
      Organization = organization
    };
    var userRole = new UserRole
    {
      Role = role
    };
    userOrganization.Roles.Add(userRole);
    organization.Users.Add(userOrganization);
    organization.Roles.Add(role);

    await this._context.SaveChangesAsync();
    return await this.GetOrganizationByIdAsync(createdOrganization.Entity.Id, requesterId);
  }

  public async Task<Organization> UpdateOrganizationAsync(long organizationId, long requesterId, UpdateRequest request)
  {
    var organization = await this.GetOrganizationByIdAsync(organizationId, requesterId);
    if (await this._context.Organizations.AnyAsync(e => e.Name == request.Name))
      throw new ServiceException(StatusCodes.Status409Conflict, ["Organization already exists."]);

    organization.Name = request.Name;
    organization.Description = request.Description;
    if (request.Image is not null)
      organization.ImageUrl = await this._cloudService.UploadImageAsync(Guid.NewGuid().ToString(), request.Image);
    organization.IsPublic = request.IsPublic!.Value;
    organization.UpdatedOnUtc = DateTime.UtcNow;

    await this._context.SaveChangesAsync();
    return await this.GetOrganizationByIdAsync(organizationId, requesterId);
  }

  public async Task DeleteOrganizationAsync(long organizationId, long requesterId)
  {
    var organization = await this.GetOrganizationByIdAsync(organizationId, requesterId);

    this._context.Organizations.Remove(organization);

    await this._context.SaveChangesAsync();
  }
}
