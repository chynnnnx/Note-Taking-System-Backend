using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FirstAngular.Application.Features.User.Queries;
using FirstAngular.Application.Features.User.Commands.UpdateUser;
using FirstAngular.Application.Features.User.Commands.ChangePassword;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UpdateUserCommand command)
        {
            var commandWithId = command with { Id = id };
            var result = await _mediator.Send(commandWithId);

            if (!result.Success)
                return BadRequest(new { message = result.Error });

            return Ok(result.Data); 
        }
        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
                return BadRequest(new { message = result.Error });
            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInfo([FromQuery] GetUserInfoQuery query)
        {
             var result = await _mediator.Send(query);
            if (!result.Success)    
                return BadRequest(new { message = result.Error });
            return Ok(result.Data);
        }

    }
}
