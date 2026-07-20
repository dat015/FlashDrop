using FlashDrop.Catalog.Application.Abstractions.Persistence;
using FlashDrop.Catalog.Application.Abstractions.Services;
using FlashDrop.Catalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Application.Features.ProductImages.AddImage
{
    public class AddImageHandler : IRequestHandler<AddImageCommand, AddImageResponse>
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IImageService _imageService;

        public AddImageHandler(IProductImageRepository productImageRepository, IImageService imageService)
        {
            _productImageRepository = productImageRepository;
            _imageService = imageService;
        }

        public async Task<AddImageResponse> Handle(AddImageCommand request, CancellationToken cancellationToken)
        {
            var imageUrl = await _imageService.UploadImageAsync(request.File, cancellationToken);

            var productImage = new ProductImage(
                request.ProductId,
                imageUrl,
                request.IsPrimary,
                request.DisplayOrder);

            await _productImageRepository.AddAsync(productImage, cancellationToken);

            return new AddImageResponse(productImage.Id);
        }
    }
}
