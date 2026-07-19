using MediatR;
using System.Collections.Generic;

namespace FlashDrop.Catalog.Application.Features.Products.GetProducts
{
    public record GetProductsQuery() : IRequest<IEnumerable<GetProductsResponse>>;
}
