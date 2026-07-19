using FlashDrop.Catalog.Application.Abstractions.Persistence;
using MediatR;
using Shared.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Application.Features.Inventory.GetInventory
{
    public class GetInventoryHandler : IRequestHandler<GetInventoryQuery, GetInventoryResponse>
    {
        private readonly IInventoryRepository _inventoryRepository;

        public GetInventoryHandler(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<GetInventoryResponse> Handle(GetInventoryQuery request, CancellationToken cancellationToken)
        {
            var inventory = await _inventoryRepository.GetByProductIdAsync(request.ProductId, cancellationToken);
            if (inventory == null)
            {
                throw new NotFoundException("Inventory not found for product");
            }

            return new GetInventoryResponse(
                inventory.Id,
                inventory.ProductId,
                inventory.Quantity,
                inventory.ReservedQuantity,
                inventory.AvailableQuantity,
                inventory.CreatedAt,
                inventory.UpdatedAt);
        }
    }
}
