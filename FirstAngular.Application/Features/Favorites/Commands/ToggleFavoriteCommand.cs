using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using FirstAngular.Application.Common.Results;

namespace FirstAngular.Application.Features.Favorites.Commands
{
    public sealed record ToggleFavoriteCommand(Guid Id) : IRequest<Result<bool>>
    {
    }
}
