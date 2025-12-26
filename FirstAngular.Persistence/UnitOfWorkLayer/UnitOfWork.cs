using FirstAngular.Application.Interfaces;
using FirstAngular.Domain.Entities;
using FirstAngular.Persistence.Data;
using FirstAngular.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Persistence.UnitOfWorkLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IBaseRepository<RefreshToken> RefreshTokenRepository { get; }
         public ICategoryRepository CategoryRepository { get; }
        public INoteRepository NoteRepository { get; }
        public IBaseRepository<AppIdentityUser> UserRepository { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            RefreshTokenRepository = new BaseRepository<RefreshToken>(_context);
            CategoryRepository = new CategoryRepository (_context);
            NoteRepository = new NoteRepository (_context);
            UserRepository = new BaseRepository<AppIdentityUser>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        } 

        public void Dispose()
        {
            _context.Dispose();
        }

    }


}
