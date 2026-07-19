using MediatR;
using Shared.Responses;
using System;

namespace FlashDrop.Catalog.Application.Features.Categories.DeleteCategory
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<DeleteCategoryResponse>;
}
