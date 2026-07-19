using System;

namespace FlashDrop.Catalog.Application.Features.Products.GetProduct
{
    public record GetProductResponse(
        Guid Id,
        Guid CategoryId,
        string Name,
        string Slug,
        string SKU,
        string? Description,
        decimal Price,
        bool IsActive,
        DateTime CreatedAt,
        DateTime? UpdatedAt);
}
