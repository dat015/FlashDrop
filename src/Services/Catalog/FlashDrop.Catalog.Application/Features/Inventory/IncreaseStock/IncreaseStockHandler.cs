using FlashDrop.Catalog.Application.Abstractions.Persistence;
using MediatR;
using Shared.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Application.Features.Inventory.IncreaseStock
{
    public class IncreaseStockHandler : IRequestHandler<IncreaseStockCommand, IncreaseStockResponse>
    {
        private readonly IInventoryRepository _inventoryRepository;

        public IncreaseStockHandler(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<IncreaseStockResponse> Handle(IncreaseStockCommand request, CancellationToken cancellationToken)
        {
            var inventory = await _inventoryRepository.GetByProductIdAsync(request.ProductId, cancellationToken);
            if (inventory == null)
            {
                throw new NotFoundException("Inventory not found for product");
            }

            inventory.IncreaseStock(request.Quantity);
            await _inventoryRepository.UpdateAsync(inventory, cancellationToken);

            return new IncreaseStockResponse(inventory.Quantity);
        }
    }
}
