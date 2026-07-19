using FlashDrop.Catalog.Application.Abstractions.Persistence;
using MediatR;
using Shared.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Application.Features.ProductImages.RemoveImage
{
    public class RemoveImageHandler : IRequestHandler<RemoveImageCommand, RemoveImageResponse>
    {
        private readonly IProductImageRepository _productImageRepository;

        public RemoveImageHandler(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }

        public async Task<RemoveImageResponse> Handle(RemoveImageCommand request, CancellationToken cancellationToken)
        {
            var image = await _productImageRepository.GetByIdAsync(request.Id, cancellationToken);
            if (image == null)
            {
                throw new NotFoundException("Product image not found");
            }

            await _productImageRepository.DeleteAsync(image, cancellationToken);

            return new RemoveImageResponse(true);
        }
    }
}
