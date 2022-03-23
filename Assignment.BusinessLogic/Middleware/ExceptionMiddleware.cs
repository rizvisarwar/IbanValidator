using Assignment.BusinessLogic.Exceptions;
using Assignment.BusinessLogic.Extensions;
using Assignment.BusinessLogic.Features.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Assignment.BusinessLogic.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            IConfiguration configuration,
            ILogger<ExceptionMiddleware> logger
            )
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Let the delegate execute down the middleware pipeline
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var httpStatusCode = HttpStatusCode.InternalServerError;
            var correlationId = httpContext.Request.Headers.GetCorrelationIdFromHeader();

            var errorResponse = new ErrorDetailsResponse
            {
                CorrelationId = correlationId
            };

            if (exception.GetType() == typeof(BusinessException))
            {
                _logger.LogWarning(correlationId, exception.Message);
                var bException = (BusinessException)exception;

                errorResponse.Code = bException.Code;
                if (bException.HttpStatusCode.HasValue)
                {
                    httpStatusCode = bException.HttpStatusCode.Value;
                }

                var message = !string.IsNullOrEmpty(exception.Message) ? exception.Message : "Internal Server Error";
                errorResponse.Messages = new List<string> { message };
            }
            else if (exception.GetType() == typeof(ValidationException))
            {
                _logger.LogWarning(correlationId, exception.Message);
                var vException = (ValidationException)exception;
                httpStatusCode = HttpStatusCode.BadRequest;
                errorResponse.Messages = vException.Errors.Select(e => e.ErrorMessage).ToList();
            }
            else
            {
                _logger.LogError(correlationId, exception);
                var message = !string.IsNullOrEmpty(exception.Message) ? exception.Message : "Internal Server Error";
                errorResponse.Messages = new List<string> { message };
            }

            errorResponse.InnerMessage = exception.InnerException?.Message;

            httpContext.Response.StatusCode = (int)httpStatusCode;
            httpContext.Response.ContentType = "application/json";
            return httpContext.Response.WriteAsync(errorResponse.SerializeObject());
        }
    }
}
