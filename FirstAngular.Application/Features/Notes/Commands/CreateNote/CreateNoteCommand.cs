using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Features.Notes.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Notes.Commands.CreateNote
{
    //public class CreateNoteCommand: IRequest<Result<NoteDTO>>
    //{
    //    public string Title { get; set; } = string.Empty;
    //    public string Content { get; set; } = string.Empty;
    //    public bool IsPinned { get; set; }
    //    public Guid? CategoryId { get; set; }
    // }
    public sealed record CreateNoteCommand(
        string Title,
        string Content,
        bool IsPinned,
        Guid? CategoryId
    ) : IRequest<Result<NoteDTO>>;

}
