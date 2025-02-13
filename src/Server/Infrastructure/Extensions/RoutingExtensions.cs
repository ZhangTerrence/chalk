using System.Reflection;
using Server.Infrastructure.Filters;

namespace Server.Infrastructure.Extensions;

internal static class RoutingExtensions
{
  public static void AddRouting(this WebApplicationBuilder builder)
  {
    builder.Services.AddCors(options => options
      .AddPolicy("CorsPolicy", policy => policy
        .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()));
    builder.Services
      .AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
      .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
    builder.Services.AddRouting(options => options.LowercaseUrls = true);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
      options.UseAllOfForInheritance();
      options.UseOneOfForPolymorphism();
      options.SelectSubTypesUsing(baseType =>
        typeof(Program).Assembly.GetTypes().Where(type => type.IsSubclassOf(baseType)));
      options.CustomSchemaIds(type => type.ToString());
      var file = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, file), true);
    });
  }

  public static void UseRouting(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI(options =>
      {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "api";
      });
    }
    app.UseHttpsRedirection();
    app.UseCors("CorsPolicy");
    app.MapControllers().AddEndpointFilterFactory(ValidationFilter.Create);
  }
}
