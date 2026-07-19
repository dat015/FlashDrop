using FlashDrop.Shared.Controller;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Api.Controllers
{
    [Authorize]
    [Route("api/product/{productId}/[controller]")]
    public class InventoryController : BaseApiController
    {
        private readonly IMediator _mediator;

        public InventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("increase")]
        public async Task<IActionResult> IncreaseStock(Guid productId, [FromBody] Application.Features.Inventory.IncreaseStock.IncreaseStockCommand command)
        {
            if (productId != command.ProductId) return BadRequest();
            var result = await _mediator.Send(command);
            return Success(result, "Stock increased successfully.");
        }

        [HttpPost("decrease")]
        public async Task<IActionResult> DecreaseStock(Guid productId, [FromBody] Application.Features.Inventory.DecreaseStock.DecreaseStockCommand command)
        {
            if (productId != command.ProductId) return BadRequest();
            var result = await _mediator.Send(command);
            return Success(result, "Stock decreased successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid productId)
        {
            var result = await _mediator.Send(new Application.Features.Inventory.GetInventory.GetInventoryQuery(productId));
            return Success(result, "Inventory retrieved successfully.");
        }
    }
}
