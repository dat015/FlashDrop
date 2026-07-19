using MediatR;
using Shared.Responses;
using System;

namespace FlashDrop.Catalog.Application.Features.Categories.UpdateCategory
{
    public record UpdateCategoryCommand(
        Guid Id,
        string Name,
        string Slug,
        string? Description) : IRequest<UpdateCategoryResponse>;
}
