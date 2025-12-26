using FirstAngular.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<CategoryEntity>
    {
        Task<List<CategoryEntity>> GetCategoriesByUserAsync(string userId, string? searchTerm = null, int pageNumber = 1,
            int pageSize = 10, CancellationToken cancellationToken = default);
    }

}
