
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_Structure.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var traceId = Guid.NewGuid();
                _logger.LogError($"Error occure while processing the request, TraceId : ${traceId}," +
                    $" Message : ${ex.Message}, StackTrace: ${ex.StackTrace}");

                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Title = "Internal Server Error",
                    Status = (int)StatusCodes.Status500InternalServerError,
                    Instance = context.Request.Path,
                    Detail = $"Internal server error occured, detail error : {ex.Message}",
                };
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
