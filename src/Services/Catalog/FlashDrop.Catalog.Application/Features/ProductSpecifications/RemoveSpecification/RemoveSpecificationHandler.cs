using FlashDrop.Catalog.Application.Abstractions.Persistence;
using MediatR;
using Shared.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Application.Features.ProductSpecifications.RemoveSpecification
{
    public class RemoveSpecificationHandler : IRequestHandler<RemoveSpecificationCommand, RemoveSpecificationResponse>
    {
        private readonly IProductSpecRepository _productSpecRepository;

        public RemoveSpecificationHandler(IProductSpecRepository productSpecRepository)
        {
            _productSpecRepository = productSpecRepository;
        }

        public async Task<RemoveSpecificationResponse> Handle(RemoveSpecificationCommand request, CancellationToken cancellationToken)
        {
            var spec = await _productSpecRepository.GetByIdAsync(request.Id, cancellationToken);
            if (spec == null)
            {
                throw new NotFoundException("Product specification not found");
            }

            await _productSpecRepository.DeleteAsync(spec, cancellationToken);

            return new RemoveSpecificationResponse(true);
        }
    }
}
