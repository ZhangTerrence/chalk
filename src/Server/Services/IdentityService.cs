using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Common.Exceptions;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.Identity;
using Server.Common.Utilities;
using Server.Data.Entities;

namespace Server.Services;

internal class IdentityService : IIdentityService
{
  private readonly SignInManager<User> _signInManager;

  public IdentityService(SignInManager<User> signInManager)
  {
    this._signInManager = signInManager;
  }

  public async Task<string> CreateUserAsync(RegisterRequest request)
  {
    var user = request.ToEntity();
    var identityResult = await this._signInManager.UserManager.CreateAsync(user, request.Password);
    if (!identityResult.Succeeded) ServiceException.InternalServerError(identityResult.GetErrorMessages());

    return await this._signInManager.UserManager.GenerateEmailConfirmationTokenAsync(user);
  }

  public async Task<User> LoginUserAsync(LoginRequest request)
  {
    var requestedUser = await this._signInManager.UserManager.FindByEmailAsync(request.Email);
    if (requestedUser is null) ServiceException.NotFound("User not found.", requestedUser);

    var result = await this._signInManager.CheckPasswordSignInAsync(requestedUser, request.Password, true);
    if (result.IsLockedOut) ServiceException.Forbidden("Too many failed attempts, account locked for 5 minutes.");
    if (!result.Succeeded) ServiceException.Forbidden("Invalid email or password.");

    var userRoles = await this._signInManager.UserManager.GetRolesAsync(requestedUser);
    await this._signInManager.SignInWithClaimsAsync(requestedUser, true, requestedUser.CreateClaims(userRoles));

    var user = await this._signInManager.UserManager.Users
      .Include(e => e.DirectMessages).ThenInclude(e => e.Channel).AsSplitQuery()
      .Include(e => e.Organizations).ThenInclude(e => e.Organization).AsSplitQuery()
      .Include(e => e.Courses).ThenInclude(e => e.Course).AsSplitQuery()
      .FirstOrDefaultAsync();
    if (user == null) ServiceException.NotFound("User not found.", user);

    return user;
  }

  public async Task<User> RefreshUserAsync(long userId)
  {
    var requestedUser = await this._signInManager.UserManager.FindByIdAsync(userId.ToString());
    if (requestedUser is null) ServiceException.NotFound("User not found.", requestedUser);

    await this._signInManager.RefreshSignInAsync(requestedUser);

    var user = await this._signInManager.UserManager.Users
      .Include(e => e.DirectMessages).ThenInclude(e => e.Channel).AsSplitQuery()
      .Include(e => e.Organizations).ThenInclude(e => e.Organization).AsSplitQuery()
      .Include(e => e.Courses).ThenInclude(e => e.Course).AsSplitQuery()
      .FirstOrDefaultAsync();
    if (user == null) ServiceException.NotFound("User not found.", user);

    return user;
  }

  public async Task LogoutUserAsync()
  {
    await this._signInManager.SignOutAsync();
  }

  public async Task ConfirmEmailAsync(string email, string token)
  {
    var user = await this._signInManager.UserManager.FindByEmailAsync(email);
    if (user is null) ServiceException.NotFound("User not found.", user);

    var identityResult = await this._signInManager.UserManager.ConfirmEmailAsync(user, token);
    if (!identityResult.Succeeded) ServiceException.Forbidden(identityResult.GetErrorMessages());

    await this._signInManager.UserManager.AddToRoleAsync(user, "User");
  }
}
