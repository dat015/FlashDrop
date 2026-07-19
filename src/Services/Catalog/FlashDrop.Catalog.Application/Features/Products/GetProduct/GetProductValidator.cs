using FluentValidation;

namespace FlashDrop.Catalog.Application.Features.Products.GetProduct
{
    public class GetProductValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
