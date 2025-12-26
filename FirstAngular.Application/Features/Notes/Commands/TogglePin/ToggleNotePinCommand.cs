using FirstAngular.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using FirstAngular.Application.Common.Results;
namespace FirstAngular.Application.Features.Notes.Commands.TogglePin
{
    public class ToggleNotePinCommand: IRequest<Result<TogglePinDTO>>
    {
        public Guid Id { get; set; }
        public bool IsPinned { get; set; }
    }
}
