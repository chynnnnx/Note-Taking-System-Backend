using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Features.User.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.User.Commands.UpdateUser
{
   public sealed record class UpdateUserCommand(
       string FirstName,
       string? MiddleInitial,
       string LastName
      
   ) : IRequest<Result<UpdateUserDTO>>;
}
