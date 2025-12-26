using FirstAngular.Application.Common.Results;
using FirstAngular.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FirstAngular.Application.Features.User.Queries
{
    public class GetUserInfoQuery: IRequest<Result<UserDTO>>
    {
        public string Id { get; set; } = string.Empty;
    }
}
