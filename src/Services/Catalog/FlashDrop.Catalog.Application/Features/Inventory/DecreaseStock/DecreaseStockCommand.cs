using MediatR;
using System;

namespace FlashDrop.Catalog.Application.Features.Inventory.DecreaseStock
{
    public record DecreaseStockCommand(Guid ProductId, int Quantity) : IRequest<DecreaseStockResponse>;
}
