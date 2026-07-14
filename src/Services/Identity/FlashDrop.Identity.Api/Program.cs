using FlashDrop.Identity.Application.Features.Register;
using FlashDrop.Services.Identity.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;
using Shared.Extensions;
using FlashDrop.Shared.Attributes;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:SecretKey"]!)),

            ClockSkew = TimeSpan.Zero
        };
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
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});
builder.Services.AddAuthorization();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(
        typeof(RegisterHandler).Assembly);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Middleware
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



app.Run();