using FlashDrop.Catalog.Application.Abstractions.Persistence;
using MediatR;
using Shared.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Application.Features.Inventory.DecreaseStock
{
    public class DecreaseStockHandler : IRequestHandler<DecreaseStockCommand, DecreaseStockResponse>
    {
        private readonly IInventoryRepository _inventoryRepository;

        public DecreaseStockHandler(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<DecreaseStockResponse> Handle(DecreaseStockCommand request, CancellationToken cancellationToken)
        {
            var inventory = await _inventoryRepository.GetByProductIdAsync(request.ProductId, cancellationToken);
            if (inventory == null)
            {
                throw new NotFoundException("Inventory not found for product");
            }

            inventory.DecreaseStock(request.Quantity);
            await _inventoryRepository.UpdateAsync(inventory, cancellationToken);

            return new DecreaseStockResponse(inventory.Quantity);
        }
    }
}
