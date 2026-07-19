using MediatR;
using System;

namespace FlashDrop.Catalog.Application.Features.ProductSpecifications.RemoveSpecification
{
    public record RemoveSpecificationCommand(Guid Id) : IRequest<RemoveSpecificationResponse>;
}
