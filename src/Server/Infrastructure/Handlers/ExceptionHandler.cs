using Amazon.Runtime;
using Serilog;
using Server.Common.Exceptions;
using Server.Common.Responses;
using IExceptionHandler = Microsoft.AspNetCore.Diagnostics.IExceptionHandler;

namespace Server.Infrastructure.Handlers;

internal class ExceptionHandler : IExceptionHandler
{
  public async ValueTask<bool> TryHandleAsync(
    HttpContext httpContext,
    Exception exception,
    CancellationToken cancellationToken
  )
  {
    switch (exception)
    {
      case ServiceException e:
        httpContext.Response.StatusCode = e.HttpStatusCode;
        await httpContext.Response
          .WriteAsJsonAsync(new Response<object>(e.Errors), cancellationToken);
        Log.Information("Service Exception: {StatusCode} {Messages}", e.HttpStatusCode, e.Errors);
        return true;
      case AmazonClientException:
      {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        Log.Error("Error connecting to AWS: {Message}", exception.Message);
        var errors = new Dictionary<string, string[]> { { "Internal Server Error.", [] } };
        await httpContext.Response.WriteAsJsonAsync(new Response<object>(errors), cancellationToken);
        return true;
      }
      default:
      {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        Log.Error("Exception: {Message}", exception.Message);
        var errors = new Dictionary<string, string[]> { { "Internal Server Error.", [] } };
        await httpContext.Response.WriteAsJsonAsync(new Response<object>(errors), cancellationToken);
        return true;
      }
    }
  }
}
