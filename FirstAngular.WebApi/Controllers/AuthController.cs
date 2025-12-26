using FirstAngular.Application.Features.Auth.Commands;
using FirstAngular.Application.Features.RefreshTokens.Commands;
using FirstAngular.Application.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FirstAngular.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
                
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(new { message = result.Error });

            return Ok(new { message = "Registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(new { message = result.Error });
            }

            return Ok(result.Data);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            try
            {
                var loginResponse = await _mediator.Send(command);
                return Ok(loginResponse);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}











//using FirstAngular.Application.Commands.Auth;
//using FirstAngular.Application.Commands.RefreshTokens;
//using FirstAngular.Application.Common.Results;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;

//namespace FirstAngular.WebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly IMediator _mediator;

//        public AuthController(IMediator mediator)
//        {
//            _mediator = mediator;
//        }

//        [HttpPost("register")]
//        public async Task<IActionResult> Register(RegisterCommand command)
//        {
//            var result = await _mediator.Send(command);
//            return result.Success ? Ok(new { message = "Registered successfully" })
//                                  : BadRequest(new { message = result.Error });
//        }

//        [HttpPost("login")]
//        public async Task<IActionResult> Login(LoginCommand command)
//        {
//            var result = await _mediator.Send(command);
//            return result.Success ? Ok(new { message = "Logged in", role = result.Data.Role })
//                                  : BadRequest(new { message = result.Error });
//        }

//        [HttpPost("refresh-token")]
//        public async Task<IActionResult> RefreshToken()
//        {
//            var refreshToken = Request.Cookies["refreshToken"];
//            if (string.IsNullOrEmpty(refreshToken))
//                return Unauthorized(new { message = "Missing refresh token" });

//            var login = await _mediator.Send(new RefreshTokenCommand { RefreshToken = refreshToken });
//            return Ok(new { message = "Refreshed" });
//        }

//        [HttpPost("logout")]
//        public IActionResult Logout()
//        {
//            Response.Cookies.Delete("accessToken");
//            Response.Cookies.Delete("refreshToken");
//            return Ok(new { message = "Logged out" });
//        }
//    }

//}
