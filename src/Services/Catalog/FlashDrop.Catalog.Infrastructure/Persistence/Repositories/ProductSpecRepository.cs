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
    public class ProductSpecRepository : IProductSpecRepository
    {
        private readonly CatalogDbContext _context;

        public ProductSpecRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<ProductSpecification?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.ProductSpecifications
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<ProductSpecification>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _context.ProductSpecifications
                .Where(x => x.ProductId == productId)
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(ProductSpecification productSpecification, CancellationToken cancellationToken = default)
        {
            await _context.ProductSpecifications.AddAsync(productSpecification, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(ProductSpecification productSpecification, CancellationToken cancellationToken = default)
        {
            _context.ProductSpecifications.Update(productSpecification);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(ProductSpecification productSpecification, CancellationToken cancellationToken = default)
        {
            _context.ProductSpecifications.Remove(productSpecification);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
