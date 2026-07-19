using MediatR;
using System;

namespace FlashDrop.Catalog.Application.Features.ProductImages.RemoveImage
{
    public record RemoveImageCommand(Guid Id) : IRequest<RemoveImageResponse>;
}
