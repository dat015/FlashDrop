using FluentValidation;

namespace FlashDrop.Catalog.Application.Features.ProductSpecifications.RemoveSpecification
{
    public class RemoveSpecificationValidator : AbstractValidator<RemoveSpecificationCommand>
    {
        public RemoveSpecificationValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
