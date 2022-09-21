using LibraryManagement.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ErrorHandling.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            context.Response.ContentType = "application/json";
            var problemDetails = new ProblemDetails
            {
                Title = exception.Message,
                Type = exception.GetType().Name,
                Instance = Guid.NewGuid().ToString()
            };
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Production")
            {
                problemDetails.Detail = exception.InnerException?.Message;
            }

            switch (exception)
            {
                case AppException e:
                    // custom application error
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    break;

                case DuplicateException e:
                    // Duplicate Exception
                    problemDetails.Status = (int)HttpStatusCode.Conflict;
                    break;

                case ArgumentException e:
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    break;

                case FileNotFoundException e:
                    problemDetails.Status = (int)HttpStatusCode.NotFound;
                    break;

                case UnauthorizedAccessException e:
                    problemDetails.Status = (int)HttpStatusCode.Unauthorized;
                    break;

                case NotImplementedException e:
                    problemDetails.Status = (int)HttpStatusCode.NotImplemented;
                    break;

                case HttpRequestException e:
                    problemDetails.Status = (int)HttpStatusCode.ServiceUnavailable;
                    break;

                default:
                    // unhandled error
                    problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            var result = JsonSerializer.Serialize(problemDetails);
            await response.WriteAsync(result);
        }
    }
}