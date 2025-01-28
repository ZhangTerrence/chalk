using Amazon.Runtime;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Responses;
using IExceptionHandler = Microsoft.AspNetCore.Diagnostics.IExceptionHandler;

namespace chalk.Server.Configurations;

public class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        switch (exception)
        {
            case ServiceException e:
                httpContext.Response.StatusCode = e.StatusCode;
                await httpContext.Response.WriteAsJsonAsync(new Response<object>([new ErrorDTO(e.Description)], null), cancellationToken);
                return true;
            case AmazonClientException:
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(new Response<object>([new ErrorDTO("Error connecting to Amazon.")], null), cancellationToken);
                return true;
            default:
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(new Response<object>([new ErrorDTO("Internal Server Error.")], null), cancellationToken);
                return true;
        }
    }
}