using FirstAngular.Application.Features.Categories.Commands.CreateCategory;
using FirstAngular.Application.Features.Categories.Commands.UpdateCategory;
using FirstAngular.Application.Features.Categories.Commands.DeleteCategory;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FirstAngular.Application.Features.Categories.Queries;

namespace FirstAngular.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success) return BadRequest(new { message = result.Error });
            return Ok(result.Data);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryCommand command)
        {
            command.Id = id; 

            var result = await _mediator.Send(command);
            if (!result.Success)
                return BadRequest(new { message = result.Error });

            return Ok(result.Data);
        }


        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] GetCategoriesByUserQuery query)
        {
            var result = await _mediator.Send(query);
            if (!result.Success)
                return BadRequest(new { message = result.Error });
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var command = new DeleteCategoryCommand { Id = id };
            var result = await _mediator.Send(command);
            if (!result.Success) return BadRequest(new { message = result.Error });
            return Ok(result.Data);
        }

    }
}
