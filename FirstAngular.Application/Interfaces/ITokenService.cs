using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(string userId, string email, IList<string> roles);
        string GenerateRefreshToken();
        string HashToken(string token);
    }
}
