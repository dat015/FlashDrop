using FlashDrop.Catalog.Domain.Persistence;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlashDrop.Catalog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogDbContext>(options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("Database"));
            });

            services.AddScoped<FlashDrop.Catalog.Application.Abstractions.Persistence.ICategoryRepository, FlashDrop.Catalog.Domain.Persistence.Repositories.CategoryRepository>();
            services.AddScoped<FlashDrop.Catalog.Application.Abstractions.Persistence.IProductRepository, FlashDrop.Catalog.Domain.Persistence.Repositories.ProductRepository>();
            services.AddScoped<FlashDrop.Catalog.Application.Abstractions.Persistence.IInventoryRepository, FlashDrop.Catalog.Domain.Persistence.Repositories.InventoryRepository>();
            services.AddScoped<FlashDrop.Catalog.Application.Abstractions.Persistence.IProductImageRepository, FlashDrop.Catalog.Domain.Persistence.Repositories.ProductImageRepository>();
            services.AddScoped<FlashDrop.Catalog.Application.Abstractions.Persistence.IProductSpecRepository, FlashDrop.Catalog.Domain.Persistence.Repositories.ProductSpecRepository>();

            return services;
        }

    }
}
