using Microsoft.EntityFrameworkCore;
namespace Event.Infrastructure.Data.Base
{
    public class BaseRepository<T>
        (EventDBContext _context)
        : IBaseRepository<T>
       where T : class
    {
        protected readonly DbSet<T> _dbSet = _context.Set<T>();
        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }
        public EventDBContext GetDbContext => _context;

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }


        public IQueryable<T> GetAllQueryAble()
        {
            return _dbSet;
        }

        public async Task<IEnumerable<T>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        }

        


        public async Task RemoveAsync(int id, CancellationToken cancellationToken)
        {
            var res = await _dbSet.FindAsync(id, cancellationToken);
            if (res != null)
            {
                _dbSet.Remove(res);
            }
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void RemoveRangeByEntitiesAsync(List<T> entities)
        {
            if (entities != null && entities.Count > 0)
            {
                _dbSet.RemoveRange(entities);
            }
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateMany(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }
    }
}
