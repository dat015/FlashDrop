using FlashDrop.Catalog.Application.Abstractions.Persistence;
using MediatR;
using Shared.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Application.Features.Products.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }

            product.Update(request.Name, request.Slug, request.Description);
            await _productRepository.UpdateAsync(product, cancellationToken);

            return new UpdateProductResponse(product.Id);
        }
    }
}
