using chalk.Server.Data;
using chalk.Server.Entities;
using chalk.Server.Services;
using chalk.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace chalk.Server.Configurations;

public static class ServiceExtensions {
  private static readonly Action<IdentityOptions> IdentityOptions = options => {
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    options.SignIn.RequireConfirmedEmail = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
  };

  private static readonly Action<CookieAuthenticationOptions> CookieAuthenticationOptions = options => {
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.MaxAge = TimeSpan.FromDays(1);
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
    options.LoginPath = "/api/account/refresh";
  };

  public static void AddAuth(this IServiceCollection services) {
    services
      .AddIdentity<User, IdentityRole<long>>()
      .AddEntityFrameworkStores<DatabaseContext>()
      .AddDefaultTokenProviders();
    services.Configure(IdentityOptions);
    services.ConfigureApplicationCookie(CookieAuthenticationOptions);
    services.AddAuthorization();
  }

  public static void AddScopedDependencies(this IServiceCollection services) {
    services.AddScoped<IEmailService, EmailService>();
    services.AddScoped<ICloudService, CloudService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<ICourseService, CourseService>();
    services.AddScoped<IOrganizationService, OrganizationService>();
    services.AddScoped<IFileService, FileService>();
  }
}