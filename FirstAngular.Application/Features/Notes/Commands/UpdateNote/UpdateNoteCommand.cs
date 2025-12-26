using FirstAngular.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstAngular.Application.DTOs;
namespace FirstAngular.Application.Features.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommand: IRequest<Result<NoteDTO>>
    {
        public Guid Id { get; set; }  
        public string? Title { get; set; }
        public string? Content { get; set; } 
        public bool IsPinned { get; set; }
        public bool IsArchived { get; set; }
        public Guid? CategoryId { get; set; } 


    }
}
