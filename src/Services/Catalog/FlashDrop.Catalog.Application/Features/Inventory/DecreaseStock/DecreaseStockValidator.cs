using FluentValidation;

namespace FlashDrop.Catalog.Application.Features.Inventory.DecreaseStock
{
    public class DecreaseStockValidator : AbstractValidator<DecreaseStockCommand>
    {
        public DecreaseStockValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
