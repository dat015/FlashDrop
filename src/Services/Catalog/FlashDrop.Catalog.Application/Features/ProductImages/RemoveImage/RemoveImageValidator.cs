using FluentValidation;

namespace FlashDrop.Catalog.Application.Features.ProductImages.RemoveImage
{
    public class RemoveImageValidator : AbstractValidator<RemoveImageCommand>
    {
        public RemoveImageValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
