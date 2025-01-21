using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Project.Domain.Abstractions;

namespace Project.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor
        (IHttpContextAccessor _httpContextAccessor): SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateEntities(DbContext? context)
        {
            if (context == null) return;
            var userId = GetCurrentUserId();
            foreach (var entry in context.ChangeTracker.Entries<IEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    entry.Entity.UpdatedBy = userId;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

        }
        #region Get Token in middleware
        /*
        public Guid GetCurrentUserId()
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
            {
                return Guid.Empty; // Trả về Guid mặc định nếu HttpContext không tồn tại
            }
            if (!_httpContextAccessor.HttpContext.Items.ContainsKey("UserId"))
            {
                return Guid.Empty; // Trả về Guid mặc định nếu không có key "UserId"
            }
            var userIdObj = _httpContextAccessor.HttpContext.Items["UserId"];
            if (userIdObj == null)
            {
                return Guid.Empty; // Trả về Guid mặc định nếu "UserId" không tồn tại
            }
            if (Guid.TryParse(userIdObj.ToString(), out var userId))
            {
                return userId; // Trả về Guid nếu chuyển đổi thành công
            }

            return Guid.Empty; // Trả về Guid mặc định nếu chuyển đổi thất bại
        }
        */
        #endregion

        #region Get token from gateways

        public Guid GetCurrentUserId()
        {
            var context = _httpContextAccessor.HttpContext;
            var userIdObj = context.Request.Headers["X-UserId"].FirstOrDefault();
            if (userIdObj is null)
                throw new Exception("Không có request header gửi từ yarp gateway");
            if (Guid.TryParse(userIdObj.ToString(), out var userId))
            {
                return userId; // Trả về Guid nếu chuyển đổi thành công
            }

            return Guid.Empty; // Trả về Guid mặc định nếu chuyển đổi thất bại
        }

        #endregion

    }
    public static class Extensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(r =>
                r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}
