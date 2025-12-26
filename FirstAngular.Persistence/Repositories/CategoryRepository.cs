using FirstAngular.Application.Interfaces;
using FirstAngular.Domain.Entities;
using FirstAngular.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FirstAngular.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        public async Task<List<CategoryEntity>> GetCategoriesByUserAsync(string userId, string? searchTerm = null, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var query = _dbSet
                .Where(c => c.UserId == userId);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return await query
                .OrderByDescending(c => c.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

    }

}
