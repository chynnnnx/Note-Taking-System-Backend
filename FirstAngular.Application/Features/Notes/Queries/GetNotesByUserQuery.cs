using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Features.Notes.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Notes.Queries
{
    //public class GetNotesByUserQuery : IRequest<Result<PagedResult<NoteDTO>>>
    //{
    //    public bool? IsPinned { get; set; }
    //    public bool? IsArchived { get; set; }
    //    public string? SearchTerm { get; set; }
    //    public int PageNumber { get; set; } = 1;
    //    public int PageSize { get; set; } = 10;
    //}

    public sealed record GetNotesByUserQuery(
        bool? IsPinned,
        bool? IsArchived,
        string? SearchTerm,
        int PageNumber = 1,
        int PageSize = 10
    ) : IRequest<Result<PagedResult<NoteDTO>>>;
}
