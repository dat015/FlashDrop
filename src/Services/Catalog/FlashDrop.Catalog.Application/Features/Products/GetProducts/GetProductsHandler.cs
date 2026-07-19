using FlashDrop.Catalog.Application.Abstractions.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Application.Features.Products.GetProducts
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<GetProductsResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<GetProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);

            var response = products.Select(p => new GetProductsResponse(
                p.Id,
                p.CategoryId,
                p.Name,
                p.Slug,
                p.SKU,
                p.Description,
                p.Price,
                p.IsActive,
                p.CreatedAt,
                p.UpdatedAt));

            return response;
        }
    }
}
