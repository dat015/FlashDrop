using FlashDrop.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Application.Abstractions.Persistence
{
    public interface IProductImageRepository
    {
        Task<ProductImage?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductImage>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task AddAsync(ProductImage productImage, CancellationToken cancellationToken = default);
        Task UpdateAsync(ProductImage productImage, CancellationToken cancellationToken = default);
        Task DeleteAsync(ProductImage productImage, CancellationToken cancellationToken = default);
    }
}
