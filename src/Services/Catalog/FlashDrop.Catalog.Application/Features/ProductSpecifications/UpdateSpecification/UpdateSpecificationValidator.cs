using FluentValidation;

namespace FlashDrop.Catalog.Application.Features.ProductSpecifications.UpdateSpecification
{
    public class UpdateSpecificationValidator : AbstractValidator<UpdateSpecificationCommand>
    {
        public UpdateSpecificationValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Value).NotEmpty().MaximumLength(500);
        }
    }
}
