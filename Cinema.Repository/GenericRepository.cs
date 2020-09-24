using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        private readonly CinemaDBContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(CinemaDBContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task Create(TEntity item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> FindById(Guid id) => await _dbSet.FindAsync(id);

        public IQueryable<TEntity> Get() => _dbSet.AsNoTracking();

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
            => _dbSet.AsNoTracking().AsEnumerable().Where(predicate).ToList();

        public async Task Remove(TEntity item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
