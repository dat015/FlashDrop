using FluentValidation;

namespace FlashDrop.Catalog.Application.Features.Inventory.IncreaseStock
{
    public class IncreaseStockValidator : AbstractValidator<IncreaseStockCommand>
    {
        public IncreaseStockValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
