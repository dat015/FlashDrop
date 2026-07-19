using FlashDrop.Catalog.Application.Abstractions.Persistence;
using FlashDrop.Catalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Application.Features.ProductSpecifications.AddSpecification
{
    public class AddSpecificationHandler : IRequestHandler<AddSpecificationCommand, AddSpecificationResponse>
    {
        private readonly IProductSpecRepository _productSpecRepository;

        public AddSpecificationHandler(IProductSpecRepository productSpecRepository)
        {
            _productSpecRepository = productSpecRepository;
        }

        public async Task<AddSpecificationResponse> Handle(AddSpecificationCommand request, CancellationToken cancellationToken)
        {
            var spec = new ProductSpecification(
                request.ProductId,
                request.Name,
                request.Value,
                request.DisplayOrder);

            await _productSpecRepository.AddAsync(spec, cancellationToken);

            return new AddSpecificationResponse(spec.Id);
        }
    }
}
