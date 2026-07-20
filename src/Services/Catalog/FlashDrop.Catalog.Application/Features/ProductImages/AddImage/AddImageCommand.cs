using FlashDrop.Catalog.Application.DTOs;
using MediatR;
using System;

namespace FlashDrop.Catalog.Application.Features.ProductImages.AddImage
{
    public record AddImageCommand(
        Guid ProductId,
        FileDto File,
        bool IsPrimary,
        int DisplayOrder) : IRequest<AddImageResponse>;
}
