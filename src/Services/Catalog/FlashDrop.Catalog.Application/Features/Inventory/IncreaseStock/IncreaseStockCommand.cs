using MediatR;
using System;

namespace FlashDrop.Catalog.Application.Features.Inventory.IncreaseStock
{
    public record IncreaseStockCommand(Guid ProductId, int Quantity) : IRequest<IncreaseStockResponse>;
}
