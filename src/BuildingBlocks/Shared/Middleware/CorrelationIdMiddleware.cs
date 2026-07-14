using FlashDrop.Shared.Constants;
using Microsoft.AspNetCore.Http;

namespace FlashDrop.Shared.Middleware;

public sealed class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string correlationId;

        if (context.Request.Headers.TryGetValue(
                CorrelationConstants.HeaderName,
                out var header))
        {
            correlationId = header!;
        }
        else
        {
            correlationId = Guid.NewGuid().ToString("N");
        }

        context.Items[CorrelationConstants.ItemName] =
            correlationId;

        context.Response.Headers[CorrelationConstants.HeaderName] =
            correlationId;

        await _next(context);
    }
}