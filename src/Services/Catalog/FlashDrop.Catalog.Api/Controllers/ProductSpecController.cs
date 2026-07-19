using FlashDrop.Shared.Controller;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Api.Controllers
{
    [Authorize]
    [Route("api/product/{productId}/spec")]
    public class ProductSpecController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ProductSpecController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid productId, [FromBody] Application.Features.ProductSpecifications.AddSpecification.AddSpecificationCommand command)
        {
            if (productId != command.ProductId) return BadRequest();
            var result = await _mediator.Send(command);
            return CreatedResponse(result, "Product specification added successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Application.Features.ProductSpecifications.UpdateSpecification.UpdateSpecificationCommand command)
        {
            if (id != command.Id) return BadRequest();
            var result = await _mediator.Send(command);
            return Success(result, "Product specification updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await _mediator.Send(new Application.Features.ProductSpecifications.RemoveSpecification.RemoveSpecificationCommand(id));
            return Success(result, "Product specification removed successfully.");
        }
    }
}
