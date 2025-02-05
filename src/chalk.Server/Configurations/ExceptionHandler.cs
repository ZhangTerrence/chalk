using Amazon.Runtime;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Responses;
using Serilog;
using IExceptionHandler = Microsoft.AspNetCore.Diagnostics.IExceptionHandler;

namespace chalk.Server.Configurations;

public class ExceptionHandler : IExceptionHandler {
  public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {
    switch (exception) {
      case ServiceException e:
        httpContext.Response.StatusCode = e.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(new Response<object>([new ErrorDTO(e.Description)], null), cancellationToken).ConfigureAwait(false);
        Log.Information("Service Exception: {StatusCode} {Description}", e.StatusCode, e.Description);
        return true;
      case AmazonClientException:
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(new Response<object>([new ErrorDTO("AWS Connection Error.")], null), cancellationToken).ConfigureAwait(false);
        return true;
      default:
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(new Response<object>([new ErrorDTO("Internal Server Error.")], null), cancellationToken).ConfigureAwait(false);
        return true;
    }
  }
}