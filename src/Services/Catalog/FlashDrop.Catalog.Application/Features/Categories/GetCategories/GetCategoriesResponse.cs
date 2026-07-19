using System;

namespace FlashDrop.Catalog.Application.Features.Categories.GetCategories
{
    public record GetCategoriesResponse(
        Guid Id,
        string Name,
        string Slug,
        string? Description,
        Guid? ParentId,
        bool IsActive,
        DateTime CreatedAt,
        DateTime? UpdatedAt);
}
