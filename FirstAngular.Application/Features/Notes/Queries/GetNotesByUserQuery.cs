using FirstAngular.Application.Common.Results;
using FirstAngular.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Notes.Queries
{
    public class GetNotesByUserQuery : IRequest<Result<List<NoteDTO>>>
    {
        public bool? IsPinned { get; set; }
        public bool? IsArchived { get; set; }
        public string? SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
