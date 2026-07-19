using FlashDrop.Catalog.Application.Abstractions.Persistence;
using FlashDrop.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Domain.Persistence.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly CatalogDbContext _context;

        public ProductImageRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<ProductImage?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.ProductImages
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<ProductImage>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _context.ProductImages
                .Where(x => x.ProductId == productId)
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(ProductImage productImage, CancellationToken cancellationToken = default)
        {
            await _context.ProductImages.AddAsync(productImage, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(ProductImage productImage, CancellationToken cancellationToken = default)
        {
            _context.ProductImages.Update(productImage);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(ProductImage productImage, CancellationToken cancellationToken = default)
        {
            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
