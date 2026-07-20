using FluentValidation;

namespace FlashDrop.Catalog.Application.Features.ProductImages.AddImage
{
    public class AddImageValidator : AbstractValidator<AddImageCommand>
    {
        public AddImageValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.File).NotNull();
            RuleFor(x => x.File.FileName).NotEmpty().When(x => x.File != null);
            RuleFor(x => x.File.Content).NotNull().When(x => x.File != null);
            RuleFor(x => x.DisplayOrder).GreaterThanOrEqualTo(0);
        }
    }
}
