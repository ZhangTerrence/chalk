using Server.Common.Interfaces;
using Server.Services;

namespace Server.Infrastructure.Extensions;

public static class ServiceExtensions
{
  public static void AddServices(this WebApplicationBuilder builder)
  {
    builder.Services.AddScoped<IEmailService, EmailService>();
    builder.Services.AddScoped<ICloudService, CloudService>();
    builder.Services.AddScoped<IIdentityService, IdentityService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<ICourseService, CourseService>();
    builder.Services.AddScoped<IModuleService, ModuleService>();
    builder.Services.AddScoped<IAssignmentGroupService, AssignmentGroupService>();
    builder.Services.AddScoped<IAssignmentService, AssignmentService>();
    builder.Services.AddScoped<IOrganizationService, OrganizationService>();
    builder.Services.AddScoped<IFileService, FileService>();
  }
}
