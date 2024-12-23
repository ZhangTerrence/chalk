using chalk.Server.DTOs.Responses;
using Microsoft.AspNetCore.Diagnostics;

namespace chalk.Server.Middleware;

public class ExceptionMiddleware : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        switch (exception)
        {
            case BadHttpRequestException e:
                httpContext.Response.StatusCode = e.StatusCode;
                break;
            default:
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        await httpContext.Response.WriteAsJsonAsync(new ApiResponseDTO<object>([exception.Message], null),
            cancellationToken);
        return true;
    }
}