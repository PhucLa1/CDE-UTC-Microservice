

using AI_LLM.Data;
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore.Storage;

namespace AI_LLM.Repository
{
    public interface IBaseRepository<T>
       where T : class
    {
        AILLMDBContext GetDbContext { get; }
        Task AddAsync(T entity, CancellationToken cancellationToken);
        Task RemoveAsync(int id, CancellationToken cancellationToken);
        void RemoveRangeByEntitiesAsync(List<T> entities);
        void Update(T entity);
        Task<int> SaveChangeAsync(CancellationToken cancellationToken);
        IQueryable<T> GetAllQueryAble();
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
        void UpdateMany(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken);
    }
}
