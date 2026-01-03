using FirstAngular.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Archives.Commands.ArchiveNote
{
    public class ArchiveNoteCommand: IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
