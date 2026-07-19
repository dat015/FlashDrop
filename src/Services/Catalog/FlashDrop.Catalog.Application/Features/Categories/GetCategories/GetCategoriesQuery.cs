using MediatR;
using Shared.Responses;
using System.Collections.Generic;

namespace FlashDrop.Catalog.Application.Features.Categories.GetCategories
{
    public record GetCategoriesQuery() : IRequest<IEnumerable<GetCategoriesResponse>>;
}
