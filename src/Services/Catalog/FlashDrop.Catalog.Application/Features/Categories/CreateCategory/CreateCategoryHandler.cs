using FlashDrop.Catalog.Application.Abstractions.Persistence;
using FlashDrop.Catalog.Domain.Entities;
using MediatR;
using Shared.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Application.Features.Categories.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CreateCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category(
                request.Name,
                request.Slug,
                request.Description,
                request.ParentId);

            await _categoryRepository.AddAsync(category, cancellationToken);

            return new CreateCategoryResponse(category.Id);
        }
    }
}
