using Server.Infrastructure.Filters;

namespace Server.Infrastructure.Extensions;

public static class RouteExtensions
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
    builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(type => type.ToString()));
  }

  public static void UseRouting(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();
    app.UseCors("CorsPolicy");
    app.MapControllers().AddEndpointFilterFactory(ValidationFilter.Create);
  }
}
