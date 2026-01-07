using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Features.Auth.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Auth.Commands
{
    //public class LoginCommand: IRequest<Result<LoginResponse>>
    //{
    //    public string Email { get; set; } = string.Empty;
    //    public string Password { get; set; } = string.Empty;
    //}

    public sealed record class LoginCommand(string Email, string Password)
        : IRequest<Result<LoginResponse>>;
}
