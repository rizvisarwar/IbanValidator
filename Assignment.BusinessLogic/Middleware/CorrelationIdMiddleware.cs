using Microsoft.AspNetCore.Http;
using Assignment.BusinessLogic.Extensions;
using System.Threading.Tasks;

namespace Assignment.BusinessLogic.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Request.Headers.EnsureCorrelationIdHeader();

            var correlationId = httpContext.Request.Headers
                                                   .GetCorrelationIdFromHeader()
                                                   .ToString();

            httpContext.Response.Headers.Add(Constants.HttpHeaderConstants.CorrelationIdHeaderKey, correlationId);

            // Let the delegate execute down the middleware pipeline
            await _next(httpContext);
        }
    }
}
