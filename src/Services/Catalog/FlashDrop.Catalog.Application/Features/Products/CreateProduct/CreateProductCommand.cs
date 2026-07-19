using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashDrop.Catalog.Application.Features.Products.CreateProduct
{
    public record CreateProductCommand(
        string Name,
        string Slug,
        string SKU,
        string? Description,
        decimal Price,
        int Quantity,
        Guid CategoryId) : IRequest<CreateProductResponse>;
    
}
