using chalk.Server.DTOs;
using chalk.Server.DTOs.Responses;
using Microsoft.AspNetCore.Diagnostics;

namespace chalk.Server.Configurations;

public class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        switch (exception)
        {
            case ServiceException e:
                httpContext.Response.StatusCode = e.StatusCode;
                await httpContext.Response.WriteAsJsonAsync(new ApiResponse<object>([new ErrorDTO(e.Description)], null), cancellationToken);
                return true;
            default:
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(new ApiResponse<object>([new ErrorDTO(exception.Message)], null), cancellationToken);
                return true;
        }
    }
}