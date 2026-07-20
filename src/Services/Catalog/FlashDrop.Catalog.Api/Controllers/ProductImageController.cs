using FlashDrop.Catalog.Application.Features.ProductImages.AddImage;
using FlashDrop.Shared.Controller;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Api.Controllers
{
    [Authorize]
    [Route("api/product/{productId}/image")]
    public class ProductImageController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ProductImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid productId, [FromForm] Microsoft.AspNetCore.Http.IFormFile file, [FromForm] bool isPrimary, [FromForm] int displayOrder)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty or missing.");
            }

            using var stream = file.OpenReadStream();
            var fileDto = new Application.DTOs.FileDto(stream, file.FileName, file.ContentType);

            var command = new Application.Features.ProductImages.AddImage.AddImageCommand(
                productId,
                fileDto,
                isPrimary,
                displayOrder);

            var result = await _mediator.Send(command);
            return CreatedResponse(result, "Product image added successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await _mediator.Send(new Application.Features.ProductImages.RemoveImage.RemoveImageCommand(id));
            return Success(result, "Product image removed successfully.");
        }
    }
}
