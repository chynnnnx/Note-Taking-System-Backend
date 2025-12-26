using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FirstAngular.Application.Features.User.Queries;
namespace FirstAngular.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetUserInfo()
        {
            var query = new GetUserInfoQuery();
            var result = await _mediator.Send(query);
            if (!result.Success)
                return BadRequest(new { message = result.Error });
            return Ok(result.Data);
        }
    }
}
