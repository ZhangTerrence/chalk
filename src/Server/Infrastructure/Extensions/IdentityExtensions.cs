using Microsoft.AspNetCore.Identity;
using Server.Data;
using Server.Data.Entities;

namespace Server.Infrastructure.Extensions;

public static class IdentityExtensions
{
  public static void AddIdentity(this WebApplicationBuilder builder)
  {
    builder.Services.AddIdentity<User, IdentityRole<long>>()
      .AddEntityFrameworkStores<DatabaseContext>()
      .AddDefaultTokenProviders();
    builder.Services.Configure<IdentityOptions>(options =>
    {
      options.User.RequireUniqueEmail = true;
      options.Password.RequireDigit = true;
      options.Password.RequireLowercase = true;
      options.Password.RequireUppercase = true;
      options.Password.RequireNonAlphanumeric = true;
      options.Password.RequiredLength = 8;
      options.SignIn.RequireConfirmedEmail = true;
      options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
      options.Lockout.MaxFailedAccessAttempts = 5;
    });
    builder.Services.ConfigureApplicationCookie(options =>
    {
      options.Cookie.HttpOnly = true;
      options.Cookie.SameSite = SameSiteMode.Strict;
      options.Cookie.MaxAge = TimeSpan.FromDays(1);
      options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
      options.LoginPath = "/api/refresh";
    });
    builder.Services.AddAuthorization();
  }

  public static void UseIdentity(this WebApplication app)
  {
    app.UseAuthentication();
    app.UseAuthorization();
  }
}
