﻿

using Auth.Data.Data;
using BuildingBlocks.Pagination;

namespace Auth.Repositories.Base
{
    public interface IBaseRepository<T>
       where T : class
    {
        AuthDBContext GetDbContext { get; }
        Task AddAsync(T entity, CancellationToken cancellationToken);
        Task RemoveAsync(int id, CancellationToken cancellationToken);
        void RemoveRangeByEntitiesAsync(List<T> entities);
        void Update(T entity);
        Task<IEnumerable<T>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<PaginationResult<T>> QueryAsync(PaginationRequest paginationRequest, CancellationToken cancellationToken);
        Task<int> SaveChangeAsync(CancellationToken cancellationToken);
        IQueryable<T> GetAllQueryAble();
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
        void UpdateMany(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);
    }
}
