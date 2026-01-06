using FirstAngular.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FirstAngular.Application.Features.User.Commands.ChangePassword
{
    public sealed record ChangePasswordCommand(string CurrentPassword, string NewPassword, string ConfirmPassword)
        : IRequest<Result<bool>>;
}
