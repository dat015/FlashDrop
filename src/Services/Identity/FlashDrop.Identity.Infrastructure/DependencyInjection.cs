
using FlashDrop.Identity.Application.Abstractions.Authentication;
using FlashDrop.Identity.Application.Abstractions.Persistence;
using FlashDrop.Identity.Infrastructure.Authentication;
using FlashDrop.Identity.Infrastructure.Persistence;
using FlashDrop.Identity.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FlashDrop.Identity.Application.Abstractions.Security;

namespace FlashDrop.Services.Identity.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("Database"));
            });
            services.AddHttpContextAccessor();
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();       
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IRefreshTokenHasher, RefreshTokenHasher>();
            return services;
        }
    }
}
