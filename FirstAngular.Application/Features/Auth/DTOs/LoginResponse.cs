using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Auth.DTOs
{
    //public class LoginResponse
    //{
    //    public string Token { get; set; } = string.Empty;
    //    public string RefreshToken { get; set; } = string.Empty;
    //    public DateTime Expiration { get; set; }
    //    public string Role { get; set; } = string.Empty;
    //}

    public sealed record LoginResponse (string Token, string RefreshToken, DateTime Expiration, string Role);
}
