using Microsoft.AspNetCore.Diagnostics;
using MKodul1.Exceptions;

namespace MKodul1.ExceptionHandlers
{
    public class CategoryValidationExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is CategoryValidationException ex)
            {
                httpContext.Response.StatusCode = 400;

                await httpContext.Response
                    .WriteAsJsonAsync(new
                    {
                        target = ex.Field,
                        description = ex.Description
                    },
                    cancellationToken);

                return true;
            }

            return false;
        }
    }
}
