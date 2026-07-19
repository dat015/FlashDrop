using MediatR;
using System;

namespace FlashDrop.Catalog.Application.Features.ProductImages.AddImage
{
    public record AddImageCommand(
        Guid ProductId,
        string ImageUrl,
        bool IsPrimary,
        int DisplayOrder) : IRequest<AddImageResponse>;
}
