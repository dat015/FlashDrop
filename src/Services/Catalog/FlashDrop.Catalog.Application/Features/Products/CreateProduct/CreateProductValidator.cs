using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashDrop.Catalog.Application.Features.Products.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(200);
            RuleFor(p => p.Slug).NotEmpty().MaximumLength(200);
            RuleFor(p => p.SKU).NotEmpty().MaximumLength(50);
            RuleFor(p => p.Price).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Quantity).GreaterThanOrEqualTo(0);
            RuleFor(p => p.CategoryId).NotEmpty();
        }
    }
}
