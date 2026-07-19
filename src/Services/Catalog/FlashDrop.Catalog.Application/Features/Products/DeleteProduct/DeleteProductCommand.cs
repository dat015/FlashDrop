using MediatR;
using System;

namespace FlashDrop.Catalog.Application.Features.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : IRequest<DeleteProductResponse>;
}
