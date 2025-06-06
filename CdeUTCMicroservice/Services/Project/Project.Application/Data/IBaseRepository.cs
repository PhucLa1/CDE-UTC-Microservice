﻿




using BuildingBlocks.Enums;
using Microsoft.EntityFrameworkCore.Storage;

namespace Project.Application.Data
{
    public interface IBaseRepository<T>
       where T : class
    {
        int GetCurrentId();
        DateDisplay GetCurrentDateDisplay();
        TimeDisplay GetCurrentTimeDisplay();
        int GetProjectId();
        Task AddAsync(T entity, CancellationToken cancellationToken);
        void Remove(T entity);
        void RemoveRangeByEntitiesAsync(List<T> entities);
        void Update(T entity);
        Task<IEnumerable<T>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);        
        Task<int> SaveChangeAsync(CancellationToken cancellationToken);
        IQueryable<T> GetAllQueryAble();
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
        void UpdateMany(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken);
    }
}
