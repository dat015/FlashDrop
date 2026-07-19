using MediatR;
using System;

namespace FlashDrop.Catalog.Application.Features.Products.GetProduct
{
    public record GetProductQuery(Guid Id) : IRequest<GetProductResponse>;
}
