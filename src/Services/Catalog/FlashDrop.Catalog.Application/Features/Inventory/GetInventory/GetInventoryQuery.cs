using MediatR;
using System;

namespace FlashDrop.Catalog.Application.Features.Inventory.GetInventory
{
    public record GetInventoryQuery(Guid ProductId) : IRequest<GetInventoryResponse>;
}
