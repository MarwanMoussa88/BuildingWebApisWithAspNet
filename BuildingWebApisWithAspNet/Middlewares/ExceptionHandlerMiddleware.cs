using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace BuildingWebApisWithAspNet.Middlewares
{
    public class ExceptionHandlerMiddleware : IExceptionHandler
    {
        private readonly IProblemDetailsService problemDetailsService;

        public ExceptionHandlerMiddleware(IProblemDetailsService problemDetailsService)
        {
            this.problemDetailsService = problemDetailsService;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var features = httpContext.Features.Get<IExceptionHandlerFeature>();

            int status = exception switch
            {
                ArgumentException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
            httpContext.Response.StatusCode = status;

            var problemDetails = new ProblemDetails()
            {
                Detail = exception.Message,
                Instance = httpContext.Request.GetDisplayUrl(),
                Status = status,
                Title = "Sever Error",
                Type = "https:/ /tools.ietf.org/html/rfc7231#section-6.6.1",

            };

            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                Exception = exception,
                HttpContext = httpContext,
                ProblemDetails = problemDetails
            });
        }
    }
}
