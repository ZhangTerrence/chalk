using chalk.Server.Configurations;
using chalk.Server.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configures logging
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
// Configures PostgreSQL database
builder.Services.AddDbContext<DatabaseContext>(options => options
  .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
  .UseSnakeCaseNamingConvention());

// Configures CORS
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", policy => policy
  .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
  .AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
// Configures authentication and authorization
builder.Services.AddAuth();
// Configures controller behavior
builder.Services
  .AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
  .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
// Configures routing behavior
builder.Services.AddRouting(options => options.LowercaseUrls = true);
// Configures FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
// Configures scoped services
builder.Services.AddScopedDependencies();
// Configures data protection
builder.Services.AddDataProtection();
// Configures Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configures exception handling
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configures the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseExceptionHandler();
app.Run();