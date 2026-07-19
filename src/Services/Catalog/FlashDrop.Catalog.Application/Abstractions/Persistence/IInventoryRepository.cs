using FlashDrop.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Application.Abstractions.Persistence
{
    public interface IInventoryRepository
    {
        Task<Inventory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Inventory?> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Inventory>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Inventory inventory, CancellationToken cancellationToken = default);
        Task UpdateAsync(Inventory inventory, CancellationToken cancellationToken = default);
        Task DeleteAsync(Inventory inventory, CancellationToken cancellationToken = default);
    }
}
