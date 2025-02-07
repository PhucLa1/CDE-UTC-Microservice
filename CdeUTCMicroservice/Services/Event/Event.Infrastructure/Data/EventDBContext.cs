using Event.Core.Entities;
using Event.Core.Entities.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Event.Infrastructure
{
    public class EventDBContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EventDBContext(DbContextOptions<EventDBContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public EventDBContext(DbContextOptions<EventDBContext> options) : base(options) { }
        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            var userId = GetCurrentUserId();

            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added))
            {
                if (entry.Entity is IAuditable)
                {
                    var entity = (IAuditable)entry.Entity;
                    entity.UpdatedAt = DateTime.UtcNow;
                    entity.UpdatedBy = userId;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = DateTime.UtcNow;
                        entity.CreatedBy = userId;
                    }
                }
            }
        }

        #region Get Token in middleware
        /*
        public int GetCurrentUserId()
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
            {
                return int.Empty; // Trả về int mặc định nếu HttpContext không tồn tại
            }
            if (!_httpContextAccessor.HttpContext.Items.ContainsKey("UserId"))
            {
                return int.Empty; // Trả về int mặc định nếu không có key "UserId"
            }
            var userIdObj = _httpContextAccessor.HttpContext.Items["UserId"];
            if (userIdObj == null)
            {
                return int.Empty; // Trả về int mặc định nếu "UserId" không tồn tại
            }
            if (int.TryParse(userIdObj.ToString(), out var userId))
            {
                return userId; // Trả về int nếu chuyển đổi thành công
            }

            return int.Empty; // Trả về int mặc định nếu chuyển đổi thất bại
        }
        */
        #endregion

        #region Get token from gateways

        public int GetCurrentUserId()
        {
            /*
            var context = _httpContextAccessor.HttpContext;
            var userIdObj = context.Request.Headers["X-UserId"].FirstOrDefault();
            if (userIdObj is null)
                return 0;
            if (int.TryParse(userIdObj.ToString(), out var userId))
            {
                return userId; // Trả về int nếu chuyển đổi thành công
            }
            */

            return 0; // Trả về int mặc định nếu chuyển đổi thất bại
        }

        #endregion

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<ActivityTypeParent> ActivityTypeParents { get; set; }
        public DbSet<Log> Logs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
