using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.User.DTOs
{
    public sealed record UpdateUserDTO(UserDTO User, bool HasChanges);
    



}
