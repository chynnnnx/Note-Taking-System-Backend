using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Notes.DTOs
{
    public sealed record  UpdateNoteDTO(Guid Id, string Title,
        string Content,
        bool IsPinned,
        bool IsArchived,
        Guid? CategoryId
    );
}
