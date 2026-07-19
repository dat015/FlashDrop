using FlashDrop.Catalog.Application.Abstractions.Persistence;
using MediatR;
using Shared.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Application.Features.Categories.GetCategories
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<GetCategoriesResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<GetCategoriesResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync(cancellationToken);
            
            var response = categories.Select(c => new GetCategoriesResponse(
                c.Id,
                c.Name,
                c.Slug,
                c.Description,
                c.ParentId,
                c.IsActive,
                c.CreatedAt,
                c.UpdatedAt
            ));

            return response;
        }
    }
}
