using FirstAngular.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Interfaces
{
    public interface INoteRepository : IBaseRepository<NoteEntity>
    {
      
        Task<(List<NoteEntity> Notes, int TotalCount)> GetNotesByUserAsync(string userId, bool? isPinned = null, bool? isArchived = null, bool? isFavorite = null,
 string? searchTerm = null,
     int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    }

}
