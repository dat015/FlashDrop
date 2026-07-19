using MediatR;
using System;

namespace FlashDrop.Catalog.Application.Features.ProductSpecifications.UpdateSpecification
{
    public record UpdateSpecificationCommand(
        Guid Id,
        string Name,
        string Value) : IRequest<UpdateSpecificationResponse>;
}
