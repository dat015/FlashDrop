using FlashDrop.Catalog.Application.Abstractions.Persistence;
using MediatR;
using Shared.Exceptions;
using Shared.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Application.Features.Categories.DeleteCategory
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<DeleteCategoryResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);
            if (category == null)
            {
                throw new NotFoundException("Category not found");
            }

            await _categoryRepository.DeleteAsync(category, cancellationToken);

            return new DeleteCategoryResponse(true);
        }
    }
}
