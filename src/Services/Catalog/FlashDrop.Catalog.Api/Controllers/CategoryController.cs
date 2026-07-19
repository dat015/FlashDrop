using FlashDrop.Shared.Controller;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CategoryController : BaseApiController
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Application.Features.Categories.CreateCategory.CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedResponse(result, "Category created successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Application.Features.Categories.UpdateCategory.UpdateCategoryCommand command)
        {
            if (id != command.Id) return BadRequest();
            var result = await _mediator.Send(command);
            return Success(result, "Category updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new Application.Features.Categories.DeleteCategory.DeleteCategoryCommand(id));
            return Success(result, "Category deleted successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new Application.Features.Categories.GetCategory.GetCategoryQuery(id));
            return Success(result, "Category retrieved successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new Application.Features.Categories.GetCategories.GetCategoriesQuery());
            return Success(result, "Categories retrieved successfully.");
        }
    }
}
