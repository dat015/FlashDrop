using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.Exceptions;
using Shared.Responses;

namespace Shared.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        HttpStatusCode statusCode;
        object response;

        switch (exception)
        {
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;

                response = ApiResponse<object>.FailResponse(
                    validationException.Message,
                    validationException.Errors);
                break;

            case BadRequestException:
                statusCode = HttpStatusCode.BadRequest;

                response = ApiResponse<object>.FailResponse(exception.Message);
                break;

            case UnauthorizedException:
                statusCode = HttpStatusCode.Unauthorized;

                response = ApiResponse<object>.FailResponse(exception.Message);
                break;

            case ForbiddenException:
                statusCode = HttpStatusCode.Forbidden;

                response = ApiResponse<object>.FailResponse(exception.Message);
                break;

            case NotFoundException:
                statusCode = HttpStatusCode.NotFound;

                response = ApiResponse<object>.FailResponse(exception.Message);
                break;

            case ConflictException:
                statusCode = HttpStatusCode.Conflict;

                response = ApiResponse<object>.FailResponse(exception.Message);
                break;

            default:
                statusCode = HttpStatusCode.InternalServerError;

                response = ApiResponse<object>.FailResponse(
                    "An unexpected error occurred.");
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}