using Microsoft.AspNetCore.Http;

namespace FlashDrop.Shared.Middleware;

public sealed class SecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public SecurityHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.OnStarting(() =>
        {
            var headers = context.Response.Headers;

            headers.TryAdd("X-Content-Type-Options", "nosniff");
            headers.TryAdd("X-Frame-Options", "DENY");
            headers.TryAdd("Referrer-Policy", "strict-origin-when-cross-origin");
            headers.TryAdd("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
            headers.TryAdd("Permissions-Policy", "camera=(), microphone=(), geolocation=()");

            return Task.CompletedTask;
        });

        await _next(context);
    }
}