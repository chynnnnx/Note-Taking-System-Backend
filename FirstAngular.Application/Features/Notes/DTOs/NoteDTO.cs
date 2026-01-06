using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Notes.DTOs
{
    //public class NoteDTO
    //{
    //    public Guid Id { get; set; }
    //    public string UserId { get; set; } = string.Empty;
    //    public string Title { get; set; } = string.Empty;
    //    public string Content { get; set; } = string.Empty;
    //    public bool IsPinned { get; set; }
    //    public bool IsArchived { get; set; }
    //    public DateTime CreatedAt { get; set; } 
    //    public DateTime UpdatedAt { get; set; }
    //    public Guid? CategoryId { get; set; }

    //}
    public sealed record NoteDTO (Guid Id,
        string UserId,
        string Title,
        string Content,
        bool IsPinned,
        bool IsArchived,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        Guid? CategoryId
    );
}
