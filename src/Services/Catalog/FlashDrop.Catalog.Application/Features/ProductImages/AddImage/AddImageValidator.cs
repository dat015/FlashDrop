using FluentValidation;

namespace FlashDrop.Catalog.Application.Features.ProductImages.AddImage
{
    public class AddImageValidator : AbstractValidator<AddImageCommand>
    {
        public AddImageValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.ImageUrl).NotEmpty().MaximumLength(500);
            RuleFor(x => x.DisplayOrder).GreaterThanOrEqualTo(0);
        }
    }
}
