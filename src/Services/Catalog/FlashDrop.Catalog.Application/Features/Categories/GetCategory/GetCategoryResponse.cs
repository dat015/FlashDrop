using System;

namespace FlashDrop.Catalog.Application.Features.Categories.GetCategory
{
    public record GetCategoryResponse(
        Guid Id,
        string Name,
        string Slug,
        string? Description,
        Guid? ParentId,
        bool IsActive,
        DateTime CreatedAt,
        DateTime? UpdatedAt);
}
