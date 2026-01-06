using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Features.Notes.DTOs;
namespace FirstAngular.Application.Features.Notes.Commands.TogglePin
{
    //public class ToggleNotePinCommand: IRequest<Result<TogglePinDTO>>
    //{
    //    public Guid Id { get; set; }
    //    public bool IsPinned { get; set; }
    //}
    public sealed record ToggleNotePinCommand(Guid Id, bool IsPinned) : IRequest<Result<TogglePinDTO>>;
}
