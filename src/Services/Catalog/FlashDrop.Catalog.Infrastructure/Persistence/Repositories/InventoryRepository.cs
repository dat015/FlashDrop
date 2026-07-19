using FlashDrop.Catalog.Application.Abstractions.Persistence;
using FlashDrop.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Domain.Persistence.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly CatalogDbContext _context;

        public InventoryRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<Inventory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Inventory
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Inventory?> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _context.Inventory
                .FirstOrDefaultAsync(x => x.ProductId == productId, cancellationToken);
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Inventory.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Inventory inventory, CancellationToken cancellationToken = default)
        {
            await _context.Inventory.AddAsync(inventory, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Inventory inventory, CancellationToken cancellationToken = default)
        {
            _context.Inventory.Update(inventory);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Inventory inventory, CancellationToken cancellationToken = default)
        {
            _context.Inventory.Remove(inventory);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
