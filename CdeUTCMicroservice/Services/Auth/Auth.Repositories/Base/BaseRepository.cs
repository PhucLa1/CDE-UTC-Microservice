using Auth.Data.Data;
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
namespace Auth.Repositories.Base
{
    public class BaseRepository<T>
        (AuthDBContext _context)
        : IBaseRepository<T>
       where T : class
    {
        protected readonly DbSet<T> _dbSet = _context.Set<T>();
        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {


            await _dbSet.AddAsync(entity, cancellationToken);
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

        public async Task<PaginationResult<T>> QueryAsync(PaginationRequest paginationRequest, CancellationToken cancellationToken)
        {
            var query = _dbSet.AsQueryable();

            // Tìm kiếm toàn cục (SearchTerm)
            if (!string.IsNullOrEmpty(paginationRequest.SearchTerm))
            {
                // Tùy chỉnh tìm kiếm theo yêu cầu
                query = query.Where(e => EF.Functions.Like(EF.Property<string>(e, "name"), $"%{paginationRequest.SearchTerm}%"));
            }

            // Lọc theo cột
            if (paginationRequest.Filters != null)
            {
                foreach (var filter in paginationRequest.Filters)
                {
                    query = query.Where($"{filter.Key}.Contains(@0)", filter.Value);
                }
            }

            // Sắp xếp
            if (!string.IsNullOrEmpty(paginationRequest.SortColumn))
            {
                query = paginationRequest.SortDescending
                    ? query.OrderByDescending(e => EF.Property<object>(e, paginationRequest.SortColumn))
                    : query.OrderBy(e => EF.Property<object>(e, paginationRequest.SortColumn));
            }

            // Tổng số bản ghi
            var totalRecords = await query.CountAsync();

            // Phân trang
            var data = await query
            .Skip((paginationRequest.PageIndex - 1) * paginationRequest.PageSize)
            .Take(paginationRequest.PageSize)
                .ToListAsync();

            // Tính toán kết quả trả về
            return new PaginationResult<T>(paginationRequest.PageIndex, paginationRequest.PageSize, totalRecords, data);
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
