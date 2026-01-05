using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FirstAngular.Application.Interfaces;
using FirstAngular.Domain.Entities;
using FirstAngular.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace FirstAngular.Persistence.Repositories
{
    public class NoteRepository:  BaseRepository<NoteEntity>, INoteRepository
    { 
     public NoteRepository(AppDbContext context) : base(context) { }

        public async Task<(List<NoteEntity> Notes, int TotalCount)> GetNotesByUserAsync(string userId, bool? isPinned = null, bool? isArchived = null, string? searchTerm = null,
          int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsNoTracking().Where(n => n.UserId == userId);

            if (isPinned.HasValue)
                query = query.Where(n => n.IsPinned == isPinned.Value);

            if (isArchived.HasValue)
                query = query.Where(n => n.IsArchived == isArchived.Value);

            if (!string.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(n =>
                    n.Title.ToLower().Contains(searchTerm.ToLower()) ||
                    n.Content.ToLower().Contains(searchTerm.ToLower()));

            var totalCount = await query.CountAsync(cancellationToken); 

            var notes = await query
                .OrderByDescending(n => n.UpdatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (notes, totalCount);
        }

    }
}
