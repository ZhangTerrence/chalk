using Server.Infrastructure.Handlers;

namespace Server.Infrastructure.Extensions;

public static class ExceptionExtensions
{
  public static void AddExceptionHandler(this WebApplicationBuilder builder)
  {
    builder.Services.AddExceptionHandler<ExceptionHandler>();
    builder.Services.AddProblemDetails();
  }
}
