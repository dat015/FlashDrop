using MediatR;
using Shared.Responses;
using System;

namespace FlashDrop.Catalog.Application.Features.Categories.CreateCategory
{
    public record CreateCategoryCommand(
        string Name,
        string Slug,
        string? Description,
        Guid? ParentId) : IRequest<CreateCategoryResponse>;
}
