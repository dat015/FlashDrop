using FluentValidation;

namespace FlashDrop.Catalog.Application.Features.Inventory.GetInventory
{
    public class GetInventoryValidator : AbstractValidator<GetInventoryQuery>
    {
        public GetInventoryValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}
