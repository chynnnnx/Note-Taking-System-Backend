using FirstAngular.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IBaseRepository<RefreshToken> RefreshTokenRepository { get; }
        INoteRepository NoteRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IBaseRepository<AppIdentityUser> UserRepository { get; }
    }
}
