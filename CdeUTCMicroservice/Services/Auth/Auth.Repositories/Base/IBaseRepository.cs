

namespace Auth.Repositories.Base
{
    public interface IBaseRepository<T>
       where T : class
    {
        Task AddAsync(T entity, CancellationToken cancellationToken);
        Task RemoveAsync(int id, CancellationToken cancellationToken);
        void RemoveRangeByEntitiesAsync(List<T> entities);
        void Update(T entity);
        Task<IEnumerable<T>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<int> SaveChangeAsync(CancellationToken cancellationToken);
        IQueryable<T> GetAllQueryAble();
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
        void UpdateMany(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);
    }
}
