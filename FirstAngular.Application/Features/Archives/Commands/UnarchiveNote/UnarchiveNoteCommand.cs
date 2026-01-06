using FirstAngular.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Archives.Commands.UnarchiveNote
{
    //public class UnarchiveNoteCommand: IRequest<Result<bool>>
    //{
    //    public Guid Id { get; set; }
    //    public string UserId { get; set; } = string.Empty;
    //}
    public sealed record UnarchiveNoteCommand(Guid Id) : IRequest<Result<bool>>;
}
