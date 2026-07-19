using MediatR;
using Shared.Responses;
using System;

namespace FlashDrop.Catalog.Application.Features.Categories.GetCategory
{
    public record GetCategoryQuery(Guid Id) : IRequest<GetCategoryResponse>;
}
