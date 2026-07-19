using FlashDrop.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Application.Abstractions.Persistence
{
    public interface IProductSpecRepository
    {
        Task<ProductSpecification?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductSpecification>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task AddAsync(ProductSpecification productSpecification, CancellationToken cancellationToken = default);
        Task UpdateAsync(ProductSpecification productSpecification, CancellationToken cancellationToken = default);
        Task DeleteAsync(ProductSpecification productSpecification, CancellationToken cancellationToken = default);
    }
}
