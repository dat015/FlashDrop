using FluentValidation;

namespace FlashDrop.Catalog.Application.Features.ProductSpecifications.AddSpecification
{
    public class AddSpecificationValidator : AbstractValidator<AddSpecificationCommand>
    {
        public AddSpecificationValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Value).NotEmpty().MaximumLength(500);
            RuleFor(x => x.DisplayOrder).GreaterThanOrEqualTo(0);
        }
    }
}
