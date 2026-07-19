using System;

namespace FlashDrop.Catalog.Application.Features.Inventory.GetInventory
{
    public record GetInventoryResponse(
        Guid Id,
        Guid ProductId,
        int Quantity,
        int ReservedQuantity,
        int AvailableQuantity,
        DateTime CreatedAt,
        DateTime? UpdatedAt);
}
