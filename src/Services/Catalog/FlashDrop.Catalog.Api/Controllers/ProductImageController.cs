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
        public async Task<IActionResult> Add(Guid productId, [FromBody] Application.Features.ProductImages.AddImage.AddImageCommand command)
        {
            if (productId != command.ProductId) return BadRequest();
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
