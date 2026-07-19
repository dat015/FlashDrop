using FluentValidation;

namespace FlashDrop.Catalog.Application.Features.Categories.DeleteCategory
{
    public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Category Id is required.");
        }
    }
}
