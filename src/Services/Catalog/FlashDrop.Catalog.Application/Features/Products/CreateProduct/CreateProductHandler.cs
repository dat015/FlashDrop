using FlashDrop.Catalog.Application.Abstractions.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashDrop.Catalog.Application.Features.Products.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public CreateProductHandler(IProductRepository productRepository, IInventoryRepository inventoryRepository)
        {
            _productRepository = productRepository;
            _inventoryRepository = inventoryRepository;
        }

        public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new FlashDrop.Catalog.Domain.Entities.Product(
                request.CategoryId,
                request.Name,
                request.Slug,
                request.SKU,
                request.Price,
                request.Description);

            await _productRepository.AddAsync(product, cancellationToken);

            var inventory = new FlashDrop.Catalog.Domain.Entities.Inventory(
                product.Id,
                request.Quantity);

            await _inventoryRepository.AddAsync(inventory, cancellationToken);

            return new CreateProductResponse(product.Id);
        }
    }
}
