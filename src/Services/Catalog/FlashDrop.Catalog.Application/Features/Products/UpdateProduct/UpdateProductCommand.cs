using MediatR;
using System;

namespace FlashDrop.Catalog.Application.Features.Products.UpdateProduct
{
    public record UpdateProductCommand(
        Guid Id,
        string Name,
        string Slug,
        string? Description) : IRequest<UpdateProductResponse>;
}
