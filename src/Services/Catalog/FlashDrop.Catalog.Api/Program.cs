using FlashDrop.Catalog.Infrastructure;
using FlashDrop.Shared.Attributes;
using Shared.Extensions;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter =
        PartitionedRateLimiter.Create<HttpContext, string>(context =>
        {
            var endpoint = context.GetEndpoint();

            var metadata = endpoint?
                .Metadata
                .GetMetadata<RateLimitAttribute>();

            var permit = metadata?.PermitLimit ?? 100;

            var window = metadata?.Window ?? TimeSpan.FromMinutes(1);

            var key =
                context.User.FindFirst("sub")?.Value ??
                context.Connection.RemoteIpAddress?.ToString() ??
                "anonymous";

            return RateLimitPartition.GetFixedWindowLimiter(
                key,
                _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = permit,
                    Window = window,
                    QueueLimit = 0
                });
        });
});
builder.Services.AddAuthorization();
//builder.Services.AddMediatR(cfg =>
//{
//    cfg.RegisterServicesFromAssembly(
//        typeof(GetProductsHandler).Assembly);
//});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapSwaggerUI();
}
app.UseCorrelationId();

app.UseRequestLogging();

app.UseSecurityHeaders();

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

app.UseGlobalExceptionMiddleware();

app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCompression();
// Controller
app.MapControllers();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.Run();