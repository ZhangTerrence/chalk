using Serilog;

namespace Server.Infrastructure.Extensions;

public static class LoggingExtensions
{
  public static void AddLogging(this WebApplicationBuilder builder)
  {
    builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
  }

  public static void UseLogging(this WebApplication app)
  {
    app.UseSerilogRequestLogging();
  }
}
