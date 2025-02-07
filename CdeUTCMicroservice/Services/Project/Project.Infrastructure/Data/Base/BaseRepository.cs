using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using Project.Application.Data;

namespace Project.Infrastructure.Data.Base
{
    public class BaseRepository<T>
        (ProjectDBContext _context, IHttpContextAccessor _httpContextAccessor)
        : IBaseRepository<T>
       where T : class
    {
        protected readonly DbSet<T> _dbSet = _context.Set<T>();
        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }
        public int GetCurrentId()
        {
            var context = _httpContextAccessor.HttpContext;
            var userIdObj = context.Request.Headers["X-UserId"].FirstOrDefault();
            if (userIdObj is null)
                return 0;
            if (int.TryParse(userIdObj.ToString(), out var userId))
            {
                return userId; // Trả về int nếu chuyển đổi thành công
            }

            return 0; // Trả về int mặc định nếu chuyển đổi thất bại
        }

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

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken);  // Lưu thay đổi vào cơ sở dữ liệu
                await transaction.CommitAsync(cancellationToken);    // Cam kết transaction
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);  // Rollback nếu có lỗi xảy ra
                throw;  // Ném lại lỗi để tầng trên xử lý
            }
        }


        public void Remove(T entity)
        {
            _dbSet.Remove(entity);

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
