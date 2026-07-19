using FlashDrop.Catalog.Application.Abstractions.Persistence;
using MediatR;
using Shared.Exceptions;
using Shared.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Application.Features.Categories.GetCategory
{
    public class GetCategoryHandler : IRequestHandler<GetCategoryQuery, GetCategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<GetCategoryResponse> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);
            if (category == null)
            {
                throw new NotFoundException("Category not found");
            }

            return new GetCategoryResponse(
                category.Id,
                category.Name,
                category.Slug,
                category.Description,
                category.ParentId,
                category.IsActive,
                category.CreatedAt,
                category.UpdatedAt);
        }
    }
}
