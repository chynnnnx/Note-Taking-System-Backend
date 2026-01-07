using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using FirstAngular.Application.Common.Results;
using FirstAngular.Domain.Entities;
namespace FirstAngular.Application.Features.Auth.Commands
{
    public class RegisterCommand: IRequest<Result<AppIdentityUser>>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string MiddleInitial { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
