using MediatR;
using System;

namespace FlashDrop.Catalog.Application.Features.ProductSpecifications.AddSpecification
{
    public record AddSpecificationCommand(
        Guid ProductId,
        string Name,
        string Value,
        int DisplayOrder) : IRequest<AddSpecificationResponse>;
}
