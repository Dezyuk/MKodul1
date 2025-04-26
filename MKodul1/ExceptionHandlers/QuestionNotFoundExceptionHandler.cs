using Microsoft.AspNetCore.Diagnostics;
using MKodul1.Exceptions;

namespace MKodul1.ExceptionHandlers
{
    public class QuestionNotFoundExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is QuestionNotFoundException ex)
            {
                httpContext.Response.StatusCode = 409;

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
