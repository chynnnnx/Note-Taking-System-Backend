using FirstAngular.Application.Common.Results;
using FirstAngular.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Notes.Commands.CreateNote
{
    public class CreateNoteCommand: IRequest<Result<NoteDTO>>
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPinned { get; set; }
        public Guid? CategoryId { get; set; }
     }
}
