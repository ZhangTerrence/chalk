using Amazon.Runtime;
using Serilog;
using Server.Common.DTOs;
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
          .WriteAsJsonAsync(new Response<object>(e.Messages.Select(x => new ErrorDto(x)), null), cancellationToken);
        Log.Information("Service Exception: {StatusCode} {Messages}", e.HttpStatusCode, e.Messages);
        return true;
      case AmazonClientException:
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        Log.Error("Error connecting to AWS: {Message}", exception.Message);
        await httpContext.Response
          .WriteAsJsonAsync(new Response<object>([new ErrorDto("Internal Server Error.")], null), cancellationToken);
        return true;
      default:
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        Log.Error("Exception: {Message}", exception.Message);
        await httpContext.Response
          .WriteAsJsonAsync(new Response<object>([new ErrorDto("Internal Server Error.")], null), cancellationToken);
        return true;
    }
  }
}
