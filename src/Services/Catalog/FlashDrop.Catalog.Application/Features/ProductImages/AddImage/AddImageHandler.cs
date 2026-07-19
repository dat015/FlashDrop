using FlashDrop.Catalog.Application.Abstractions.Persistence;
using FlashDrop.Catalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Application.Features.ProductImages.AddImage
{
    public class AddImageHandler : IRequestHandler<AddImageCommand, AddImageResponse>
    {
        private readonly IProductImageRepository _productImageRepository;

        public AddImageHandler(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }

        public async Task<AddImageResponse> Handle(AddImageCommand request, CancellationToken cancellationToken)
        {
            var productImage = new ProductImage(
                request.ProductId,
                request.ImageUrl,
                request.IsPrimary,
                request.DisplayOrder);

            await _productImageRepository.AddAsync(productImage, cancellationToken);

            return new AddImageResponse(productImage.Id);
        }
    }
}
