using FirstAngular.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using FirstAngular.Application.Features.User.DTOs;

namespace FirstAngular.Application.Features.User.Queries
{
    public sealed record GetUserInfoQuery() : IRequest<Result<UserDTO>>;

}
