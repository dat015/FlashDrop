using FlashDrop.Catalog.Application.Abstractions.Persistence;
using MediatR;
using Shared.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Application.Features.ProductSpecifications.UpdateSpecification
{
    public class UpdateSpecificationHandler : IRequestHandler<UpdateSpecificationCommand, UpdateSpecificationResponse>
    {
        private readonly IProductSpecRepository _productSpecRepository;

        public UpdateSpecificationHandler(IProductSpecRepository productSpecRepository)
        {
            _productSpecRepository = productSpecRepository;
        }

        public async Task<UpdateSpecificationResponse> Handle(UpdateSpecificationCommand request, CancellationToken cancellationToken)
        {
            var spec = await _productSpecRepository.GetByIdAsync(request.Id, cancellationToken);
            if (spec == null)
            {
                throw new NotFoundException("Product specification not found");
            }

            spec.Update(request.Name, request.Value);
            await _productSpecRepository.UpdateAsync(spec, cancellationToken);

            return new UpdateSpecificationResponse(spec.Id);
        }
    }
}
