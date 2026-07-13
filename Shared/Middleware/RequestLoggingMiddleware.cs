using FlashDrop.Shared.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace FlashDrop.Shared.Middleware;

public sealed class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var watch = Stopwatch.StartNew();

        try
        {
            await _next(context);
        }
        finally
        {
            watch.Stop();

            var correlationId =
                context.Items[CorrelationConstants.ItemName]?.ToString();

            var userId =
                context.User.FindFirst("sub")?.Value ??
                "Anonymous";

            var ip =
                context.Connection.RemoteIpAddress?.ToString();

            _logger.LogInformation(
                """
                HTTP Request

                CorrelationId : {CorrelationId}

                Method        : {Method}

                Path          : {Path}

                Query         : {Query}

                StatusCode    : {StatusCode}

                UserId        : {UserId}

                IP            : {IP}

                Elapsed(ms)   : {Elapsed}
                """,
                correlationId,
                context.Request.Method,
                context.Request.Path,
                context.Request.QueryString,
                context.Response.StatusCode,
                userId,
                ip,
                watch.ElapsedMilliseconds);
        }
    }
}