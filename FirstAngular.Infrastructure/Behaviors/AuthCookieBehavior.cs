using FirstAngular.Application.DTOs;
using FirstAngular.Application.Features.Auth.Commands;
using FirstAngular.Application.Features.RefreshTokens.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Infrastructure.Behaviors
{
    public class AuthCookieBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IConfiguration _config;

        public AuthCookieBehavior(IHttpContextAccessor httpContext, IConfiguration config)
        {
            _httpContext = httpContext;
            _config = config;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();

            if ((request is LoginCommand || request is RefreshTokenCommand) && response is LoginResponse login)
            {
                var ctx = _httpContext.HttpContext;

                ctx.Response.Cookies.Append(
                    "accessToken",
                    login.Token,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = login.Expiration
                    }
                );

                ctx.Response.Cookies.Append(
                    "refreshToken",
                    login.RefreshToken,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddDays(double.Parse(_config["Jwt:RefreshTokenDays"]))
                    }
                );
            }

            return response;
        }
    }

}
