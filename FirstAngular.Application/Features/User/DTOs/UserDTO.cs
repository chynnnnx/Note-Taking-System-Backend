using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.User.DTOs;

public sealed record UserDTO(
    string Id,
    string FirstName,
    string? MiddleInitial,
    string LastName,
    string Email
 )
{
    public string FullName =>
        string.IsNullOrWhiteSpace(MiddleInitial)
            ? $"{FirstName} {LastName}"
            : $"{FirstName} {MiddleInitial}. {LastName}";
}

