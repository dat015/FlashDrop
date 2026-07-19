using FlashDrop.Shared.Attributes;
using FlashDrop.Shared.Controller;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashDrop.Catalog.Api.Controllers
{
    [Authorize]
    public class ProductController : BaseApiController
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [RateLimit(5)]
        public async Task<IActionResult> Add([FromBody] FlashDrop.Catalog.Application.Features.Products.CreateProduct.CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedResponse(result, "Product created successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] FlashDrop.Catalog.Application.Features.Products.UpdateProduct.UpdateProductCommand command)
        {
            if (id != command.Id) return BadRequest();
            var result = await _mediator.Send(command);
            return Success(result, "Product updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new FlashDrop.Catalog.Application.Features.Products.DeleteProduct.DeleteProductCommand(id));
            return Success(result, "Product deleted successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new FlashDrop.Catalog.Application.Features.Products.GetProduct.GetProductQuery(id));
            return Success(result, "Product retrieved successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new FlashDrop.Catalog.Application.Features.Products.GetProducts.GetProductsQuery());
            return Success(result, "Products retrieved successfully.");
        }
    }
}
